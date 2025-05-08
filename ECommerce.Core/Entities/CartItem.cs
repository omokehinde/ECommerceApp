using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Core.Entities;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; } // Foreign key to Product
    public int Quantity { get; set; }

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;
}