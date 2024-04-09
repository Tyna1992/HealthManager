namespace HealthManagerServer.Service;

public record UserResponse(string Id, string UserName, string Email, string PhoneNumber, string Gender, double Weight);