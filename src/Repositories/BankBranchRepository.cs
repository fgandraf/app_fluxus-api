using MySql.Data.MySqlClient;
using FluxusApi.Models;
using System.Collections;
using Dapper;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories;

public class BankBranchRepository : Repository<BankBranch>, IBankBranchRepository
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