using ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Data;

public static class DataSeeder
{
    public static void SeedData(AppDbContext context)
    {
        // Seed Products
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Wireless Headphones",
                    Price = 99.99m,
                    Description = "Noise-canceling Bluetooth headphones",
                    ImageUrl = "https://example.com/headphones.jpg"
                },
                new Product
                {
                    Name = "Smartwatch",
                    Price = 199.50m,
                    Description = "Fitness tracking & heart rate monitor",
                    ImageUrl = "https://example.com/smartwatch.jpg"
                }
            };
            context.Products.AddRange(products);
            context.SaveChanges(); // Products get actual IDs here
            
            // Use the generated Product IDs for cart items
            var productIds = products.Select(p => p.Id).ToList();
            
            var cartItems = new List<CartItem>
            {
                new CartItem 
                {
                    ProductId = products[0].Id,
                    Quantity = 2,
                    Product = products[0] // Explicitly set navigation property
                },
                new CartItem 
                {
                    ProductId = products[1].Id,
                    Quantity = 1,
                    Product = products[1]
                }
            };
            context.CartItems.AddRange(cartItems);
            context.SaveChanges();
        }
    }
}