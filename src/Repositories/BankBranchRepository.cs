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

        public override IEnumerable GetAll()
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

            try
            {
                return Connection.Query(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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

            try
            {
                return Connection.Query(query, new { branchNumber });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
