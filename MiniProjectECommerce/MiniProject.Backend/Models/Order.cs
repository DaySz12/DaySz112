using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject.Backend.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        // Foreign Key (ไม่ผูกกับตารางแล้ว แต่ยังเก็บ ID ได้)
        [Column("customer_id")]
        public int? CustomerId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        // ----------------------------------------------------
        // ✅ เพิ่ม Field ที่สำคัญกลับเข้ามา
        // ----------------------------------------------------
        [Column("total_amount", TypeName = "numeric(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column("shipping_address")]
        public string ShippingAddress { get; set; } = string.Empty; // ที่อยู่จัดส่ง

        [Required]
        [Column("order_status")]
        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending"; // สถานะเริ่มต้น
        // ----------------------------------------------------

        // Navigation property: รายการสินค้าในคำสั่งซื้อ
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}