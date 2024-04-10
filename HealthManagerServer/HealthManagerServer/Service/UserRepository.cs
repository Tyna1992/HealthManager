using HealthManagerServer.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthManagerServer.Service;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponse>> GetAllUsers()
    {
        return await _context.Users
            .Select(user => new UserResponse(user.Id, user.UserName, user.Email, user.Gender, user.Weight))
            .ToListAsync();
    }

    public async Task<UserResponse> GetUserById(string id)
    {
        var user = await _context.Users.FindAsync(id);
        return new UserResponse(user.Id, user.UserName, user.Email, user.Gender, user.Weight);
    }

    public async Task<UserResponse> GetByEmail(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        return new UserResponse(user.Id, user.UserName, user.Email, user.Gender, user.Weight);
    }

    public async Task UpdateUser(string id, UserResponse userResponse)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user != null)
        {
            user.UserName = userResponse.UserName;
            user.Email = userResponse.Email;
            user.Gender = userResponse.Gender;
            user.Weight = userResponse.Weight;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUser(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}