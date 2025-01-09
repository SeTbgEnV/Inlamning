using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MormorDagnysInlämning.Entities;

namespace MormorDagnysInlämning.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Salesperson> Salespeople { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(Product => Product.Salesperson)
                .WithMany(Salesperson => Salesperson.Products)
                .HasForeignKey(Product => Product.SalesRepId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(OrderItem => new { OrderItem.ProductId, OrderItem.SalesOrderId });
        }
    }
}