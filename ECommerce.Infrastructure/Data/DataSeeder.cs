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
        }

        // Seed CartItems (optional)
        if (!context.CartItems.Any())
        {
            var cartItems = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 2 }, // Links to Wireless Headphones
                new CartItem { ProductId = 2, Quantity = 1 }  // Links to Smartwatch
            };
            context.CartItems.AddRange(cartItems);
        }

        context.SaveChanges();
    }
}