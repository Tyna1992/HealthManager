using HealthManagerServer.Data;
using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Service;

public class MealPlanRepository : IMealPlanRepository
{
    private readonly DataBaseContext _dataBaseContext;
    private readonly INutritionRepository _nutritionRepository;

    public MealPlanRepository(DataBaseContext dataBaseContext, INutritionRepository nutritionRepository)
    {
        _dataBaseContext = dataBaseContext;
        _nutritionRepository = nutritionRepository;
    }

    public async Task<IEnumerable<MealPlan>> GetAllMealPlans()
    {
        var mealPlans = await _dataBaseContext.MealPlans.ToListAsync();
        foreach (var mealPlan in mealPlans)
        {
            mealPlan.Meal = await _nutritionRepository.GetById(mealPlan.MealId);
        }
        return mealPlans;
    }

    public async Task<MealPlan> GetByDate(DateOnly date, string userName)
    {
        var formattedDate = date.ToDateTime(TimeOnly.MinValue);
        MealPlan? mealPlan = await _dataBaseContext.MealPlans.FirstOrDefaultAsync(x => x.Date.Date == formattedDate.Date && x.UserName == userName);
        if (mealPlan != null)
        {
            mealPlan.Meal = await _nutritionRepository.GetById(mealPlan.MealId);
        }
        return mealPlan;
    }

    public async Task<IEnumerable<MealPlan>> GetMealPlansByDay(string day, string userName)
    {
        var mealPlans = await GetAllMealPlans();
        mealPlans = mealPlans.Where(x => x.Date.DayOfWeek.ToString() == day && x.UserName == userName);
        foreach (var mealPlan in mealPlans)
        {
            mealPlan.Meal = await _nutritionRepository.GetById(mealPlan.MealId);
        }
        return mealPlans;
    }

    public async Task<IEnumerable<MealPlan>> GetMealPlansByUserName(string userName)
    {
        var mealPlans = await _dataBaseContext.MealPlans.Where(x => x.UserName == userName).ToListAsync();
        foreach (var mealPlan in mealPlans)
        {
            mealPlan.Meal = await _nutritionRepository.GetById(mealPlan.MealId);
        }
        return mealPlans;
    }

    public async Task AddMealPlan(MealPlan mealPlan)
    {
        await _dataBaseContext.MealPlans.AddAsync(mealPlan);
        await _dataBaseContext.SaveChangesAsync();
    }

    public async Task UpdateMealPlan(string id, MealPlan mealPlan)
    {
        var mealPlanToUpdate = await _dataBaseContext.MealPlans.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (mealPlanToUpdate != null)
        {
            mealPlanToUpdate.Date = mealPlan.Date;
            mealPlanToUpdate.MealTime = mealPlan.MealTime;
            _dataBaseContext.MealPlans.Update(mealPlanToUpdate);
            await _dataBaseContext.SaveChangesAsync();
        }
    }

    public async Task DeleteMealPlan(string id)
    {
        var mealPlanToDelete = await _dataBaseContext.MealPlans.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (mealPlanToDelete != null)
        {
            _dataBaseContext.MealPlans.Remove(mealPlanToDelete);
            await _dataBaseContext.SaveChangesAsync();
        }
    }
}