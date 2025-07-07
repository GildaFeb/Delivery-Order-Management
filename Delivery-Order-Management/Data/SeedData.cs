using Delivery_Order_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Delivery_Order_Management.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            await context.Database.MigrateAsync();

            // User
            if (!userManager.Users.Any())
            {
                var defaultUser = new IdentityUser
                {
                    UserName = "admin@demo.com",
                    Email = "admin@demo.com",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(defaultUser, "Admin@123");
            }

            // Customers
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>();
                for (int i = 1; i <= 10; i++)
                {
                    customers.Add(new Customer
                    {
                        Name = $"Customer {i}",
                        Address = $"Address {i}",
                        Phone = $"091700000{i:D2}"
                    });
                }
                context.Customers.AddRange(customers);
                await context.SaveChangesAsync();
            }

            // Items
            if (!context.Items.Any())
            {
                var units = new[] { "PCS", "CTN", "ROLL" };
                var statuses = new[] { "ACTIVE", "INACTIVE" };

                var items = new List<Item>();
                for (int i = 1; i <= 10; i++)
                {
                    items.Add(new Item
                    {
                        ItemCode = $"ITM{i:D3}",
                        Description = $"Sample Item {i}",
                        UnitOfMeasure = units[i % units.Length],
                        Status = statuses[i % statuses.Length]
                    });
                }
                context.Items.AddRange(items);
                await context.SaveChangesAsync();
            }

            // Delivery Orders
            if (!context.DeliveryOrders.Any())
            {
                var customerIds = context.Customers.Select(c => c.Id).Take(10).ToList();
                var deliveryOrders = new List<DeliveryOrder>();
                for (int i = 1; i <= 10; i++)
                {
                    deliveryOrders.Add(new DeliveryOrder
                    {
                        OrderNumber = $"DO{i:D3}",
                        OrderDate = DateTime.Now.AddDays(-i),
                        DeliveryDate = DateTime.Now.AddDays(i % 3),
                        CustomerId = customerIds[i % customerIds.Count],
                        Status = (i % 3 == 0) ? "PENDING" :
                         (i % 3 == 1) ? "READY TO SHIP" :
                         "DELIVERED",
                        DeliveryTiming = (i % 2 == 0) ? "AM" : "PM"
                    });
                }
                context.DeliveryOrders.AddRange(deliveryOrders);
                await context.SaveChangesAsync();
            }

            // Delivery Order Items
            if (!context.DeliveryOrderItems.Any())
            {
                var orders = context.DeliveryOrders.Take(10).ToList();
                var items = context.Items.Take(10).ToList();
                var orderItems = new List<DeliveryOrderItem>();

                for (int i = 0; i < 10; i++)
                {
                    var order = orders[i % orders.Count];
                    var item = items[i % items.Count];

                    orderItems.Add(new DeliveryOrderItem
                    {
                        DeliveryOrderId = order.DeliveryOrderId,
                        ItemId = item.ItemId,
                        Quantity = 5 + i
                    });
                }
                context.DeliveryOrderItems.AddRange(orderItems);
                await context.SaveChangesAsync();
            }
        }
    }
}
