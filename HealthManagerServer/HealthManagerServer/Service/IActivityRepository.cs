using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface IActivityRepository
{
    IEnumerable<Activity> GetAll();
    Activity? GetByActivityName(string name);
    void AddActivity(Activity activity);
    void DeleteActivity(Activity activity);
    void UpdateActivity(Activity activity);
    
}