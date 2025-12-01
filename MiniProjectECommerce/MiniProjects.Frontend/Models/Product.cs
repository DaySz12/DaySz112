
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProjects.Frontend.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; } // Primary Key


        [Required] // บังคับว่าต้องมีชื่อ
        [Column("name")]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column("price", TypeName = "numeric(10, 2)")] // แมปกับ NUMERIC ใน PostgreSQL
        public decimal Price { get; set; }

        [Column("description")]
        public string? Description { get; set; } // รายละเอียดสินค้า (อนุญาตให้เป็นค่าว่าง)

        [Column("stock_quantity")]
        public int StockQuantity { get; set; }

        [Column("category")]
        [MaxLength(100)]
        public string? Category { get; set; }

        [Column("image_url")]
        [MaxLength(255)]
        public string? ImageUrl { get; set; }
        // -----------------------------------------------------------------

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (จำเป็นสำหรับการสร้าง OrderItems)
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}