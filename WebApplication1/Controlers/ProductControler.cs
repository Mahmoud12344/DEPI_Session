using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(StoreDBContext context) : ControllerBase
{
     [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();

        var list = new List<productCategoryDTO>();
        foreach (var item in products){
            var pddto = new productCategoryDTO() {
                ProductId = item.Id,
                Productame = item.Name,
                CategoryId = item.Category.Id,
                ProductDescription = item.Description,
                CategoryName = item.Category.Name
            };
            list.Add(pddto);

        }
        return Ok(list);
    }

     [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        return Ok(product);
    }

     [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
         if (product.Category.Id > 0)
        {
            var categoryExists = await context.Categories.AnyAsync(c => c.Id == product.Category.Id);
            if (!categoryExists)
            {
                return BadRequest(new { message = $"Category with ID {product.Category.Id} does not exist." });
            }
        }

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

     [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product updatedProduct)
    {
        if (id != updatedProduct.Id && updatedProduct.Id != 0)
        {
            return BadRequest(new { message = "ID in URL does not match ID in payload." });
        }

        var existingProduct = await context.Products.FindAsync(id);
        if (existingProduct is null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

         if (updatedProduct.Category.Id> 0 && updatedProduct.Category.Id != existingProduct.Category.Id)
        {
            var categoryExists = await context.Categories.AnyAsync(c => c.Id == updatedProduct.Category.Id);
            if (!categoryExists)
            {
                return BadRequest(new { message = $"Category with ID {updatedProduct.Category.Id} does not exist." });
            }
            existingProduct.Category.Id= updatedProduct.Category.Id;
        }

        // Apply fields to the tracked entity
        existingProduct.Name = updatedProduct.Name;
        existingProduct.Description = updatedProduct.Description;
        existingProduct.Price = updatedProduct.Price;

        await context.SaveChangesAsync();

        return NoContent(); // Standard 204 response for successful PUT updates
    }

    // DELETE: api/products/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound(new { message = $"Product with ID {id} not found." });
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return NoContent(); // Standard 204 response for successful deletion
    }
}