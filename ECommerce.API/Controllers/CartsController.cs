using System.ComponentModel.DataAnnotations;
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
    // [HttpPost]
    // public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
    // {
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);

    //     var product = await _context.Products.FindAsync(cartItem.ProductId);
    //     if (product == null)
    //         return NotFound("Product not found");

    //     _context.CartItems.Add(cartItem);
    //     await _context.SaveChangesAsync();
    //     return CreatedAtAction(nameof(AddToCart), cartItem);
    // }

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

    // POST: api/carts
    [HttpPost]
    public async Task<IActionResult> UpdateCartQuantity([FromBody] CartUpdateRequest request)
    {
        // Add validation for negative quantity creation
        if (request.QuantityChange < 0)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == request.ProductId);
            if (existingItem == null)
                return BadRequest("Cannot decrease quantity for non-existent item");
        }

        // Existing logic
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.ProductId == request.ProductId);

        if (cartItem == null)
        {
            cartItem = new CartItem 
            { 
                ProductId = request.ProductId, 
                Quantity = request.QuantityChange 
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity += request.QuantityChange;
            if (cartItem.Quantity <= 0)
                _context.CartItems.Remove(cartItem);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Error saving to database");
        }

        return Ok(cartItem);
    }

    public class CartUpdateRequest
    {
        public int ProductId { get; set; }
        public int QuantityChange { get; set; }
    }

    // Clear the cart
    [HttpDelete("clear")]
    public async Task<IActionResult> ClearCart()
    {
        var items = await _context.CartItems.ToListAsync();
        _context.CartItems.RemoveRange(items);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Get all cart items
    // This endpoint retrieves all items in the cart
    [HttpGet]
    public async Task<IActionResult> GetCartItems()
    {
        var cartItems = await _context.CartItems
            .Include(ci => ci.Product)
            .ToListAsync();
        return Ok(cartItems);
    }
}