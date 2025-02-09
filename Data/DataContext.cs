using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ViktorEngmanInlämning.Entities;

namespace ViktorEngmanInlämning.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Salesperson> Salespeople { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.ProductId, o.SalesOrderId });
            modelBuilder.Entity<SupplierProduct>().HasKey(s => new { s.ProductId, s.SalespersonId });
            modelBuilder.Entity<CustomerAddress>().HasKey(c => new { c.AddressId, c.CustomerId });
            modelBuilder.Entity<SupplierAddress>().HasKey(s => new { s.AddressId, s.SupplierId });

            base.OnModelCreating(modelBuilder);
        }
    }
}