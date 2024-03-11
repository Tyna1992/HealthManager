using HealthManagerServer.Data;
using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public class ActivityRepository : IActivityRepository
{
    private readonly DataBaseContext _context;
    
    public ActivityRepository(DataBaseContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Activity> GetAll()
    {
        return _context.Activities.ToList();
    }

    public IQueryable<Activity> GetByActivityName(string name, int weight, int duration)
    {
        return _context.Activities.Where(a => a.Name.Contains(name) && a.Duration_minutes == duration && a.Weight == weight);
    }

    public void AddActivity(Activity activity)
    {
        _context.Activities.Add(activity);
        _context.SaveChanges();
    }

    public void DeleteActivity(Activity activity)
    {
        _context.Activities.Remove(activity);
        _context.SaveChanges();
    }

    public void UpdateActivity(Activity activity)
    {
        _context.Activities.Update(activity);
        _context.SaveChanges();
    }
}