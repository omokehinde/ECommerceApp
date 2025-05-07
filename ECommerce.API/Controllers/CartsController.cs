using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CartsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/carts
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = await _context.Products.FindAsync(cartItem.ProductId);
        if (product == null)
            return NotFound("Product not found");

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(AddToCart), cartItem);
    }

    // DELETE: api/carts/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
            return NotFound();

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // PUT: api/carts/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItem updatedItem)
    {
        if (id != updatedItem.Id)
            return BadRequest();

        var existingItem = await _context.CartItems.FindAsync(id);
        if (existingItem == null)
            return NotFound();

        existingItem.Quantity = updatedItem.Quantity;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}