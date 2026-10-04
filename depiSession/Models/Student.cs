using System.ComponentModel.DataAnnotations;

namespace depiSession.Models;

public class Student {
    public int Id{ get; set; }
    [StringLength(30)]
    public string Name{ get; set; }
    public string Email{ get; set; }
    public int Age{ get; set; }
    public string Department{ get; set; }
    
    
}