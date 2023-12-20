using Dapper;
using FluxusApi.Models;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories;

public class UserRepository: Repository<User>, IUserRepository
{
    protected UserRepository(MySqlConnection connection) : base(connection) { }
    
    public async Task<User> GetByUserNameAsync(string userName)
    {
        const string query = @"
                SELECT 
                    * 
                FROM 
                    User 
                WHERE 
                    UserName = @userName";

        return await Connection.QueryFirstAsync(query, new { userName });
    }
}