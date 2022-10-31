using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.Entities;
using System.Diagnostics;
using System.Reflection.Emit;

namespace NLayerApp.DAL.EF
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                DataInitializer(context);

                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var rolesManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer(userManager, rolesManager);
            }
            catch (Exception)
            {
            }
        }

        private static async Task RoleInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[] { "Administrator", "Courier" };

            foreach (string role in roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    foreach (string role in roles)
                    {
                        await userManager.AddToRoleAsync(admin, role);
                    }
                }
            }
        }

        private static void DataInitializer(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any customers.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            //initialize db

            var customers = new Customer[]
            {
                new Customer { Name = "Kirill", Address = "Moskow" },
                new Customer { Name = "Kostya", Address = "Grodno" },
                new Customer { Name = "Sergey", Address = "Minsk, Skripnikova" }
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order { Customer = customers[0], Number = 1, IsActive = true },
                new Order { Customer = customers[0], Number = 2, IsActive = true },
                new Order { Customer = customers[0], Number = 3, IsActive = true },
                new Order { Customer = customers[1], Number = 4, IsActive = true },
                new Order { Customer = customers[1], Number = 5, IsActive = true },
                new Order { Customer = customers[2], Number = 6, IsActive = true }
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();

            var shops = new Shop[]
            {
                new Shop { Name = "Fix Price", Address = "Minsk" },
                new Shop { Name = "Gippo", Address = "Vitebsk" },
                new Shop { Name = "Evroopt", Address = "Gomel" }
            };
            foreach (Shop s in shops)
            {
                context.Shops.Add(s);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product { Shop = shops[0], Name = "Orange", Price = 12345, Color = 123, IsDeleted = false },
                new Product { Shop = shops[0], Name = "Milk", Price = 252453, Color = 345, IsDeleted = false },
                new Product { Shop = shops[1], Name = "Pen", Price = 35437, Color = 546, IsDeleted = false },
                new Product { Shop = shops[1], Name = "Pencil case", Price = 46534, Color = 4534, IsDeleted = false },
                new Product { Shop = shops[2], Name = "Fork", Price = 454543, Color = 456546, IsDeleted = false },
                new Product { Shop = shops[2], Name = "Spoon", Price = 23, Color = 3543, IsDeleted = false }
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            var productOrders = new ProductOrder[]
            {
                new ProductOrder { Order = orders[0], Product = products[0] },
                new ProductOrder { Order = orders[0], Product = products[1] },
                new ProductOrder { Order = orders[0], Product = products[2] },
                new ProductOrder { Order = orders[0], Product = products[3] },

                new ProductOrder { Order = orders[1], Product = products[0] },
                new ProductOrder { Order = orders[1], Product = products[1] },
                new ProductOrder { Order = orders[1], Product = products[2] },
                new ProductOrder { Order = orders[1], Product = products[3] },
                new ProductOrder { Order = orders[1], Product = products[4] },

                new ProductOrder { Order = orders[2], Product = products[0] },
                new ProductOrder { Order = orders[2], Product = products[2] },
                new ProductOrder { Order = orders[2], Product = products[3] },
                new ProductOrder { Order = orders[2], Product = products[4] },
                new ProductOrder { Order = orders[2], Product = products[5] },

                new ProductOrder { Order = orders[3], Product = products[2] },
                new ProductOrder { Order = orders[3], Product = products[3] },
                new ProductOrder { Order = orders[3], Product = products[4] },

                new ProductOrder { Order = orders[4], Product = products[1] },

                new ProductOrder { Order = orders[5], Product = products[5] }
            };
            foreach (ProductOrder po in productOrders)
            {
                context.ProductOrders.Add(po);
            }
            context.SaveChanges();
        }
    }
}
