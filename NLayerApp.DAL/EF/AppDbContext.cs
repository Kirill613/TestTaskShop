using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.EF
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Make Number field Unique in Orders
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.Number)
                .IsUnique();

            // Add m2m ProductOrders
            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Product)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(po => po.Order)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.OrderId);

            /*//initialize db
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Kirill", Address = "Moskow" },
                new Customer { Id = 2, Name = "Kostya", Address = "Grodno" },
                new Customer { Id = 3, Name = "Sergey", Address = "Minsk, Skripnikova" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, Number = 1, IsActive = true },
                new Order { Id = 2, CustomerId = 1, Number = 2, IsActive = true },
                new Order { Id = 3, CustomerId = 1, Number = 3, IsActive = true },
                new Order { Id = 4, CustomerId = 2, Number = 4, IsActive = true },
                new Order { Id = 5, CustomerId = 2, Number = 5, IsActive = true },
                new Order { Id = 6, CustomerId = 3, Number = 6, IsActive = true }
                );

            modelBuilder.Entity<Shop>().HasData(
                new Shop { Id = 1, Name = "Fix Price", Address = "Minsk" },
                new Shop { Id = 2, Name = "Gippo", Address = "Vitebsk" },
                new Shop { Id = 3, Name = "Evroopt", Address = "Gomel" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ShopId = 1, Name = "Orange", Price = 12345, Color = 123, IsDeleted = false },
                new Product { Id = 2, ShopId = 1, Name = "Milk", Price = 252453, Color = 345, IsDeleted = false },
                new Product { Id = 3, ShopId = 2, Name = "Pen", Price = 35437, Color = 546, IsDeleted = false },
                new Product { Id = 4, ShopId = 2, Name = "Pencil case", Price = 46534, Color = 4534, IsDeleted = false },
                new Product { Id = 5, ShopId = 3, Name = "Fork", Price = 454543, Color = 456546, IsDeleted = false },
                new Product { Id = 6, ShopId = 3, Name = "Spoon", Price = 23, Color = 3543, IsDeleted = false }
            );

            modelBuilder.Entity<Product>().HasData(
                new ProductOrder { OrderId = 1, ProductId = 1 },
                new ProductOrder { OrderId = 1, ProductId = 2 },
                new ProductOrder { OrderId = 1, ProductId = 3 },
                new ProductOrder { OrderId = 1, ProductId = 4 },

                new ProductOrder { OrderId = 2, ProductId = 1 },
                new ProductOrder { OrderId = 2, ProductId = 2 },
                new ProductOrder { OrderId = 2, ProductId = 3 },
                new ProductOrder { OrderId = 2, ProductId = 4 },
                new ProductOrder { OrderId = 2, ProductId = 5 },

                new ProductOrder { OrderId = 3, ProductId = 1 },
                new ProductOrder { OrderId = 3, ProductId = 3 },
                new ProductOrder { OrderId = 3, ProductId = 4 },
                new ProductOrder { OrderId = 3, ProductId = 5 },
                new ProductOrder { OrderId = 3, ProductId = 6 },

                new ProductOrder { OrderId = 4, ProductId = 3 },
                new ProductOrder { OrderId = 4, ProductId = 4 },
                new ProductOrder { OrderId = 4, ProductId = 5 },

                new ProductOrder { OrderId = 5, ProductId = 2 },

                new ProductOrder { OrderId = 6, ProductId = 6 }
            );*/
        }
    }
}
