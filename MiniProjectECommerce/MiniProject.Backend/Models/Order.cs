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

        // Foreign Key
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column("total_amount", TypeName = "numeric(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Column("shipping_address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required]
        [Column("order_status")]
        [MaxLength(50)]
        public string OrderStatus { get; set; } = "Pending";

        // Navigation property: เชื่อมไปยัง Customer และ OrderItems
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}