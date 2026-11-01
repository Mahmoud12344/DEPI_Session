using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication1.Models;

public class Category
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [ValidateNever]
    public ICollection<Product>? Products { get; set; }
}
