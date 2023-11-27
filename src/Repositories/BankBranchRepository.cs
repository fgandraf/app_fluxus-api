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

            return _connection.QueryFirst<BankBranch>(query, new { id = id });
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

            return _connection.Query(query);
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

                return _connection.QueryFirst(query, new { id = id });
        }
    }
}
