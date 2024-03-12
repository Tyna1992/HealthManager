using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string Gender { get; set; }
    [Required]
    public double Weight { get; set; }
}