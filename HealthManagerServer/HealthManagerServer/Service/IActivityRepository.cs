using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface IActivityRepository
{
    IEnumerable<Activity> GetAll();
    IQueryable<Activity> GetByActivityName(string name, int weight = 160, int duration = 60);
    void AddActivity(Activity activity);
    void DeleteActivity(Activity activity);
    void UpdateActivity(Activity activity);
    
}