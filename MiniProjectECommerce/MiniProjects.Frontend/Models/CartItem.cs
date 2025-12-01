using System.ComponentModel.DataAnnotations;

namespace MiniProjects.Frontend.Models // ใช้ Namespace ที่ถูกต้อง
{
    // Model ที่ใช้สำหรับเก็บ Item ในตะกร้า
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1; // เริ่มต้นที่ 1

        // คำนวณราคารวมของ Item นี้
        public decimal TotalPrice => Price * Quantity;
    }
}