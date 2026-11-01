using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace depiSession.Models;

public class Product {

    public int? Id{ get; set; }
    public string Name{ get; set; }
    public string Description{ get; set; }
    public decimal Price{ get; set; }
    public int? CategoryId{ get; set; }
    public Category? Category{ get; set; }
    
    
}
