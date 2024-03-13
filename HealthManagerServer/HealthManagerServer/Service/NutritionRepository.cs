


using HealthManagerServer.Data;

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

    public void DeleteNutrition(Nutrition nutrition)
    {
        _context.Nutritions.Remove(nutrition);
        _context.SaveChanges();
    }

    public IEnumerable<Nutrition> GetAll()
    {
        return _context.Nutritions.ToList();
    }

    public Nutrition? GetByNameAndWeight(string name, double weight)
    {
        return _context.Nutritions.FirstOrDefault(nutrition => nutrition.Name == name && nutrition.Serving_size_g == weight);
    }

    public void UpdateNutrition(Nutrition nutrition)
    {
        _context.Nutritions.Update(nutrition);
        _context.SaveChanges();
    }
}