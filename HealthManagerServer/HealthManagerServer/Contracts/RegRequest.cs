using System.ComponentModel.DataAnnotations;

namespace HealthManagerServer.Contracts;
public record RegistrationRequest(
    [Required]string Email, 
    [Required]string Username, 
    [Required]string Password,
    [Required]double Weight,
    [Required]string Gender);