namespace HealthManagerServer.Model;

public class MealPlan
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public int MealId { get; set; }
    public Nutrition? Meal { get; set; }
    public DateTime Date { get; set; }
    public string? MealTime { get; set; }
    public string? DayOfTheWeek => Date.DayOfWeek.ToString("D");
}