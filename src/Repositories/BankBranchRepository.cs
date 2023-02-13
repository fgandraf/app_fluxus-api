using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class BankBranchRepository : Repository<BankBranch>
    {
        public BankBranchRepository(MySqlConnection connection) : base(connection) { }

        public IEnumerable GetIndex()
        {
            string query = @"
                SELECT 
                    Id, 
                    BranchNumber, 
                    Name, 
                    City , 
                    Phone1, 
                    Email
                FROM 
                    BankBranch 
                ORDER BY 
                    BranchNumber";

            return _connection.Query(query);
        }


        public IEnumerable GetContacts(string branchNumber)
        {
            string query = @"
                SELECT 
                    BranchNumber, 
                    Name, 
                    Phone1, 
                    Email 
                FROM 
                    BankBranch 
                WHERE 
                    BranchNumber = @branchNumber";

                return _connection.Query(query, new { branchNumber });
        }
    }
}
