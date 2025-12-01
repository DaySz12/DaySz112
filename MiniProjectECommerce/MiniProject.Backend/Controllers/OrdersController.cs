using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject.Backend.Data;
using MiniProject.Backend.Models;
using System.Linq;

namespace MiniProject.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders (ดึงรายการคำสั่งซื้อทั้งหมดสำหรับหน้าประวัติ)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            // ใช้ Include() เพื่อดึงข้อมูล Customer มาพร้อมกัน
            return await _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // POST: api/Orders (สำหรับยืนยันการสั่งซื้อจากตะกร้าสินค้า)
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            // **ส่วน Logic การตรวจสอบสินค้าในสต็อกและคำนวณราคา** (สำคัญด้านความปลอดภัย)
            decimal calculatedTotal = 0;

            // 1. ตรวจสอบและลดสต็อก
            foreach (var item in order.OrderItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null || product.StockQuantity < item.Quantity)
                {
                    return BadRequest($"สินค้า {item.ProductId} ไม่มีในสต็อกพอหรือหาไม่พบ");
                }

                // กำหนดราคาต่อหน่วย ณ เวลาที่สั่งซื้อ และคำนวณยอดรวม
                item.UnitPrice = product.Price;
                calculatedTotal += product.Price * item.Quantity;

                // ลดจำนวนสต็อกสินค้า
                product.StockQuantity -= item.Quantity;
            }

            // 2. กำหนดค่าเริ่มต้นของคำสั่งซื้อ
            order.TotalAmount = calculatedTotal;
            order.OrderDate = DateTime.UtcNow;
            order.OrderStatus = "Processing";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // ส่ง HTTP 201 Created กลับไป
            return CreatedAtAction(nameof(GetOrders), new { id = order.OrderId }, order);
        }
    }
}