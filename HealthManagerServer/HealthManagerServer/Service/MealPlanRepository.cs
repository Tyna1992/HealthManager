using HealthManagerServer.Data;
using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Service;

public class MealPlanRepository : IMealPlanRepository
{
    private readonly UserContext _userContext;
    private readonly DataBaseContext _dataBaseContext;

    public MealPlanRepository(UserContext userContext, DataBaseContext dataBaseContext)
    {
        _userContext = userContext;
        _dataBaseContext = dataBaseContext;
    }

    public async Task<IEnumerable<MealPlan>> GetAllMealPlans()
    {
        var mealPlans = await _dataBaseContext.MealPlans.ToListAsync();
        return mealPlans;
    }

    public async Task<MealPlan> GetByDate(DateOnly date)
    {
        var formatedDate = date.ToDateTime(TimeOnly.MinValue);
        MealPlan? mealPlan = await _dataBaseContext.MealPlans.FirstOrDefaultAsync(x => x.Date.Date == formatedDate.Date);
        return mealPlan;
    }

    public async Task<IEnumerable<MealPlan>> GetMealPlansByDay(string day)
    {
        var mealPlans = await _dataBaseContext.MealPlans.Where(x => x.DayOfTheWeek == day).ToListAsync();
        return mealPlans;
    
    }

    public async Task<IEnumerable<MealPlan>> GetMealPlansByUserId(string userId)
    {
        var mealPlans = await _dataBaseContext.MealPlans.Where(x => x.UserId == userId).ToListAsync();
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
    