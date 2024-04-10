using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface IMealPlanRepository
{
    Task<IEnumerable<MealPlan>> GetAllMealPlans();
    Task<MealPlan> GetByDate(DateOnly date);
    Task<IEnumerable<MealPlan>> GetMealPlansByDay(string day);
    Task<IEnumerable<MealPlan>> GetMealPlansByUserId(string userId);
    Task AddMealPlan(MealPlan mealPlan);
    Task UpdateMealPlan(string id, MealPlan mealPlan);
    Task DeleteMealPlan(string id);
}