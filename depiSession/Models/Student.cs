using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace depiSession.Models;

public class Student {
    [Key]
    [ValidateNever]
    public int? Id{ get; set; }
    [StringLength(30)]
    public string Name{ get; set; }
    public string Email{ get; set; }
    public int Age{ get; set; }
    public string Department{ get; set; }
    
    
}