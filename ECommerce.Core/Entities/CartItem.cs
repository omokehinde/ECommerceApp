namespace ECommerce.Core.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; } // Foreign key to Product
    public int Quantity { get; set; }
}