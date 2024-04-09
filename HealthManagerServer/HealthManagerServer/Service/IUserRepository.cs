namespace HealthManagerServer.Service;

    public interface IUserRepository
    {
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<UserResponse> GetUserById(string id);
        Task<UserResponse> GetByEmail(string email);
        Task UpdateUser(string id, UserResponse userResponse);
        Task DeleteUser(string id);
    }
