using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject.Backend.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [Column("first_name")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("last_name")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty; // Unique key ถูกกำหนดใน SQL

        [Column("phone_number")]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}