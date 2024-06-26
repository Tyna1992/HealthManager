using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface INutritionRepository
{
    IEnumerable<Nutrition> GetAll();
    Nutrition? GetByNameAndWeight(string name, double weight);
    void AddNutrition(Nutrition nutrition);
    Task DeleteNutrition(int id);
    Task UpdateNutrition(int id, Nutrition nutrition);
    Task<Nutrition> GetById(int id);
}