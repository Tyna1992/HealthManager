public interface INutritionRepository
{
    IEnumerable<Nutrition> GetAll();
    Nutrition? GetByNameAndWeight(string name, double weight);
    void AddNutrition(Nutrition nutrition);
    Task DeleteNutrition(int id);
    Task UpdateNutrition(int id);
}