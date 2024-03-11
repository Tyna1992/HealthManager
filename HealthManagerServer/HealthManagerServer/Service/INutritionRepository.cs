public interface INutritionRepository
{
    IEnumerable<Nutrition> GetAll();
    Nutrition? GetByNameAndWeight(string name, double weight);
    void AddNutrition(Nutrition nutrition);
    void DeleteNutrition(Nutrition nutrition);
    void UpdateNutrition(Nutrition nutrition);
}