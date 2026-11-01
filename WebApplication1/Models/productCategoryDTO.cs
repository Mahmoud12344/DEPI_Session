namespace WebApplication1.Models;

public class productCategoryDTO {
    public int? ProductId{ get; set; }
    public string? Productame{ get; set; }
    public string? ProductDescription{ get; set; }
    public decimal ProductPrice{ get; set; }
    
    public Category? Category{ get; set; }
    
    public int? CategoryId{ get; set; }
    public string? CategoryName { get; set; }
}