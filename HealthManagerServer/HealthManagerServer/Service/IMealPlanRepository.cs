using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface IMealPlanRepository
{
    Task<IEnumerable<MealPlan>> GetAllMealPlans();
    Task<MealPlan> GetByDate(DateOnly date, string userName);
    Task<IEnumerable<MealPlan>> GetMealPlansByDay(string day, string userName);
    Task<IEnumerable<MealPlan>> GetMealPlansByUserName(string userName);
    Task AddMealPlan(MealPlan mealPlan);
    Task UpdateMealPlan(string id, MealPlan mealPlan);
    Task DeleteMealPlan(string id);
}