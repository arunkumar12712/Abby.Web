using System.ComponentModel.DataAnnotations;

namespace Abby.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Display(Name = "Display Name")]
    [Range(1, 100, ErrorMessage = "Display ode much be range 1-100!!!")]
    public int DisplayOrder { get; set; }
}

