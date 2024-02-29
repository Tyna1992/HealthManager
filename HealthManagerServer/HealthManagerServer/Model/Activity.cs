namespace HealthManagerServer.Model;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Calories_per_hour { get; set; }
    public int Weight { get; set; }
    public int Duration_minutes { get; set; }
    public double Total_calories { get; set; }
}