using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class BankBranchRepository : Repository<BankBranch>
    {
        public BankBranchRepository(MySqlConnection connection) : base(connection) { }

        public BankBranch Get(string id)
        {
            string query = @"
                SELECT 
                    *
                FROM 
                    BankBranch 
                WHERE 
                    Id = @id";

            return Connection.QueryFirst<BankBranch>(query, new { id = id });
        }
        
        public IEnumerable GetIndex()
        {
            string query = @"
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

            return Connection.Query(query);
        }
        
        public async Task<IEnumerable> GetIndexAsync()
        {
            string query = @"
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


        public IEnumerable GetContacts(string id)
        {
            string query = @"
                SELECT 
                    Id, 
                    Name, 
                    Phone1, 
                    Email 
                FROM 
                    BankBranch 
                WHERE 
                    Id = @id";

                return Connection.QueryFirst(query, new { id = id });
        }
        
        public async Task<IEnumerable> GetContactsAsync(string id)
        {
            string query = @"
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
}
