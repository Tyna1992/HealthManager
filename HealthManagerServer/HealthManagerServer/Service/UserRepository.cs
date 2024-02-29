using dotenv.net;
using HealthManagerServer.Model;

namespace HealthManagerServer.Service;

using System.Collections.Generic;
using HealthManagerServer.Data;
using Microsoft.Data.SqlClient;


public class UserRepository : IUserRepository
{
    private readonly DataBaseContext _context;
    
    public UserRepository(DataBaseContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        _context.Remove(user);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetByUserName(string userName)
    {
        return _context.Users.FirstOrDefault(user => user.UserName == userName);
    }

    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email);
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);    
        _context.SaveChanges();
    }
}