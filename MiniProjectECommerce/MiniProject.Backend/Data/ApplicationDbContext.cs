using Microsoft.EntityFrameworkCore;
using MiniProject.Backend.Models;


namespace MiniProject.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // -------------------------------------------------------------------
        // ต้องประกาศ DbSet สำหรับทุก Entity Model ที่จะใช้ใน Controller
        // -------------------------------------------------------------------
        public DbSet<Product> Products { get; set; }  // สำหรับตาราง Products
        public DbSet<Customer> Customers { get; set; } // สำหรับตาราง Customers (ที่คุณเพิ่งเพิ่ม)
        public DbSet<Order> Orders { get; set; }      // สำหรับตาราง Orders
        public DbSet<OrderItem> OrderItems { get; set; } // สำหรับตาราง OrderItems

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // กำหนดความสัมพันธ์ระหว่าง Orders, OrderItems และ Products
            // เพื่อช่วยให้ EF Core สามารถโหลดข้อมูลเชื่อมโยงได้ง่ายขึ้น (e.g., .Include())
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            base.OnModelCreating(modelBuilder);
        }
    }
}