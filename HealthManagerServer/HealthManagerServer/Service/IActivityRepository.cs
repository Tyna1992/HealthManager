using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

public interface IActivityRepository
{
    Task<IEnumerable<Activity>> GetAll();
    IList<Activity> GetByActivityName(string name, int weight, int duration);
    void AddActivity(Activity activity);
    Task DeleteActivity(int id);
    Task UpdateActivity(int id, Activity activity);
    
}