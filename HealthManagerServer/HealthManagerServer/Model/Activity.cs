namespace HealthManagerServer.Model;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double CaloriesBurnedPerHour { get; set; }
    public int Duration { get; set; }
    public double TotalCaloriesBurned { get; set; }
}