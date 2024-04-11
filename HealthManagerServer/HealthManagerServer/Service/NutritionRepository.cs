using HealthManagerServer.Data;
using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Service;

public class NutritionRepository : INutritionRepository
{
    private readonly DataBaseContext _context;

    public NutritionRepository(DataBaseContext context)
    {
        _context = context;
    }
    public void AddNutrition(Nutrition nutrition)
    {
        _context.Nutritions.Add(nutrition);
        _context.SaveChanges();
    }

    public async Task DeleteNutrition(int id)
    {
        var nutrition = await _context.Nutritions.FirstOrDefaultAsync(nutrition => nutrition.Id == id);
        if (nutrition != null)
        {
            _context.Nutritions.Remove(nutrition);
            await _context.SaveChangesAsync();
        }
    }

    public IEnumerable<Nutrition> GetAll()
    {
        return _context.Nutritions.ToList();
    }

    public async Task<Nutrition> GetById(int id)
    {
        var result = await _context.Nutritions.FirstOrDefaultAsync(nutrition => nutrition.Id == id);
        return result;
    }
    public Nutrition? GetByNameAndWeight(string name, double weight)
    {
        return _context.Nutritions.FirstOrDefault(nutrition => nutrition.Name == name && nutrition.Serving_size_g == weight);
    }

    public async Task UpdateNutrition(int id, Nutrition nutrition)
    {
        var nutritionToUpdate = await _context.Nutritions.FirstOrDefaultAsync(nutrition => nutrition.Id == id);
        if (nutritionToUpdate != null)
        {
            nutritionToUpdate.Name = nutrition.Name;
            nutritionToUpdate.Serving_size_g = nutrition.Serving_size_g;
            nutritionToUpdate.Calories = nutrition.Calories;
            nutritionToUpdate.Fat_total_g = nutrition.Fat_total_g;
            nutritionToUpdate.Fat_saturated_g = nutrition.Fat_saturated_g;
            nutritionToUpdate.Protein_g = nutrition.Protein_g;
            nutritionToUpdate.Sodium_mg = nutrition.Sodium_mg;
            nutritionToUpdate.Potassium_mg = nutrition.Potassium_mg;
            nutritionToUpdate.Cholesterol_mg = nutrition.Cholesterol_mg;
            nutritionToUpdate.Carbohydrates_total_g = nutrition.Carbohydrates_total_g;
            nutritionToUpdate.Fiber_g = nutrition.Fiber_g;
            nutritionToUpdate.Sugar_g = nutrition.Sugar_g;
            _context.Nutritions.Update(nutritionToUpdate);
            await _context.SaveChangesAsync();
        }
    }
}