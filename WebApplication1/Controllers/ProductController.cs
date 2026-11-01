using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ProductController(StoreDBContext context) : Controller
{
    // GET: / or /Product or /Product/Index
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .ToListAsync();

        return View(products);
    }

    // GET: /Product/Add
    [HttpGet]
    public IActionResult Add()
    {
        return View(new Product { Category = new Category() });
    }

    // POST: /Product/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Product p)
    {
        if (p.Category == null || string.IsNullOrWhiteSpace(p.Category.Name))
        {
            ModelState.AddModelError("Category.Name", "Category name is required.");
        }

        if (string.IsNullOrWhiteSpace(p.Name))
        {
            ModelState.AddModelError("Name", "Product name is required.");
        }

        if (p.Price < 0)
        {
            ModelState.AddModelError("Price", "Price must be non-negative.");
        }

        if (!ModelState.IsValid)
        {
            return View(p);
        }

        var catName = p.Category!.Name.Trim();
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == catName.ToLower());
        if (category == null)
        {
            category = new Category { Name = catName };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        p.Id = null; // Ensure new identity generated
        p.Category = category;
        p.CategoryId = category.Id;

        context.Products.Add(p);
        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        if (product.Category == null)
        {
            product.Category = new Category();
        }

        return View(product);
    }

    // POST: /Product/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product p)
    {
        if (p.Category == null || string.IsNullOrWhiteSpace(p.Category.Name))
        {
            ModelState.AddModelError("Category.Name", "Category name is required.");
        }

        if (string.IsNullOrWhiteSpace(p.Name))
        {
            ModelState.AddModelError("Name", "Product name is required.");
        }

        if (p.Price < 0)
        {
            ModelState.AddModelError("Price", "Price must be non-negative.");
        }

        if (!ModelState.IsValid)
        {
            return View(p);
        }

        var existingProduct = await context.Products
            .Include(pr => pr.Category)
            .FirstOrDefaultAsync(pr => pr.Id == p.Id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        var catName = p.Category!.Name.Trim();
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == catName.ToLower());
        if (category == null)
        {
            category = new Category { Name = catName };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        existingProduct.Name = p.Name;
        existingProduct.Description = p.Description;
        existingProduct.Price = p.Price;
        existingProduct.Category = category;
        existingProduct.CategoryId = category.Id;

        await context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // POST: /Product/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
