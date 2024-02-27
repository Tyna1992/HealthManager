

using HealthManagerServer.Model;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User? GetByUserName(string userName);
    void AddUser(User user);
    void DeleteUser(User user);
    void UpdateUser(User user);
    User? GetByEmail(string email);
    
}
