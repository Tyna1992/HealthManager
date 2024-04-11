using HealthManagerServer.Data;
using HealthManagerServer.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Service;

public class ActivityRepository : IActivityRepository
{
    private readonly DataBaseContext _context;
    
    public ActivityRepository(DataBaseContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Activity>> GetAll()
    {
        return await _context.Activities.ToListAsync();
    }

    public IList<Activity> GetByActivityName(string name, int weight, int duration)
    {
        return _context.Activities.Where(a => a.Name.Contains(name) && a.Duration_minutes == duration && a.Weight == weight).ToList();
    }

    public void AddActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        _context.SaveChanges();
    }

    public async Task DeleteActivity(int id)
    {
        var activity = await _context.Activities.FirstOrDefaultAsync(activity => activity.Id == id);
        if (activity != null)
        {
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task UpdateActivity(int id, Activity activity)
    {
        var activityToUpdate = await _context.Activities.FirstOrDefaultAsync(activity => activity.Id == id);
        if (activityToUpdate != null)
        {
            activityToUpdate.Name = activity.Name;
            activityToUpdate.Calories_per_hour = activity.Calories_per_hour;
            activityToUpdate.Weight = activity.Weight;
            activityToUpdate.Duration_minutes = activity.Duration_minutes;
            activityToUpdate.Total_calories = activity.Total_calories;
            _context.Activities.Update(activityToUpdate);
            await _context.SaveChangesAsync();
        }
    }
}