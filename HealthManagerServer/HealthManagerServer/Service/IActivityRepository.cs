using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface IActivityRepository
{
    IEnumerable<Activity> GetAll();
    IList<Activity> GetByActivityName(string name, int weight, int duration);
    void AddActivity(Activity activity);
    void DeleteActivity(Activity activity);
    void UpdateActivity(Activity activity);
    
}