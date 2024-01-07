using System.Collections;
using Dapper;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories.Database;

public class BankBranchRepository : Repository<BankBranchDTO>, IBankBranchRepository
{
    public BankBranchRepository(MySqlConnection connection) : base(connection) { }
        
    public async Task<IEnumerable> GetIndexAsync()
    {
        const string query = @"
                SELECT
                    Id, 
                    Name, 
                    City , 
                    Phone1, 
                    Email
                FROM 
                    BankBranch 
                ORDER BY 
                    Id";

        return await Connection.QueryAsync(query);
    }
        
    public async Task<IEnumerable> GetContactsAsync(string id)
    {
        const string query = @"
                SELECT 
                    Id, 
                    Name, 
                    Phone1, 
                    Email 
                FROM 
                    BankBranch 
                WHERE 
                    Id = @id";

        return await Connection.QueryFirstAsync(query, new { id = id });
    }
             
}