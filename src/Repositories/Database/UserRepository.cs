using System.Collections;
using Dapper;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories.Database;

public class UserRepository: Repository<UserDTO>, IUserRepository
{
    public UserRepository(MySqlConnection connection) : base(connection) { }
    
    public async Task<UserDTO> GetByUserNameAsync(string userName)
    {
        const string query = @"
                SELECT 
                    * 
                FROM 
                    User 
                WHERE 
                    UserName = @userName";

        return await Connection.QueryFirstOrDefaultAsync<UserDTO>(query, new { userName });
    }

    public async Task<UserDTO> GetByProfessionalIdAsync(int professionalId)
    {
        const string query = @"
                SELECT 
                    * 
                FROM 
                    User 
                WHERE 
                    ProfessionalId = @professionalId";

        return await Connection.QueryFirstAsync(query, new { professionalId });
    }
}