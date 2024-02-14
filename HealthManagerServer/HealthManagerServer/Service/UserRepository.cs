using HealthManagerServer.Model;

namespace HealthManagerServer.Service;
using Microsoft.Data.SqlClient;


public class UserRepository
{
    private readonly string _connectionString;
    
    public UserRepository()
    {
        _connectionString = File.ReadAllText("env/env.txt");
    }
    
    private SqlConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
    
    private void ExecuteNonQuery(string query)
    {
        using var connection = GetConnection();
        using var command = new SqlCommand(query, connection);
        command.ExecuteNonQuery();
    }
    
    private static SqlCommand CreateCommand(SqlConnection connection, string query)
    {
        return new SqlCommand
        {
            CommandText = query,
            Connection = connection,
        };
    }

    public void CreateUserTable()
    {
        var query = @"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users')
              BEGIN
                  CREATE TABLE Users (Id UNIQUEIDENTIFIER PRIMARY KEY, 
                                      UserName NVARCHAR(255), 
                                      Email NVARCHAR(255), 
                                      Password NVARCHAR(255), 
                                      Weight FLOAT,
                                      Gender NVARCHAR(10))
              END";
        ExecuteNonQuery(query);
        
    }
    
    public void AddUser(User user)
    {
        var query = @"INSERT INTO Users (Id, UserName, Email, Password, Weight, Gender) 
                  VALUES (@Id, @UserName, @Email, @Password, @Weight, @Gender)";
    
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Weight", user.Weight);
                var genderString= user.Gender.ToString();
                command.Parameters.AddWithValue("@Gender", genderString);
            
                command.ExecuteNonQuery();
            }
        }
    }
}