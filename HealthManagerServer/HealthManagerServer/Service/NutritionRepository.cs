
using System.Threading.Tasks;

using HealthManagerServer.Data;
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

    public Nutrition? GetByNameAndWeight(string name, double weight)
    {
        return _context.Nutritions.FirstOrDefault(nutrition => nutrition.Name == name && nutrition.Serving_size_g == weight);
    }

    public async Task UpdateNutrition(int id)
    {
        var nutrition = await _context.Nutritions.FirstOrDefaultAsync(nutrition => nutrition.Id == id);
        if (nutrition != null)
        {
            _context.Nutritions.Update(nutrition);
            await _context.SaveChangesAsync();
        }
    }
}