namespace HealthManagerServer.Model;

public record MealPlanRequest(string UserName, double ServingSize, string Name, DateOnly Date, string MealTime);