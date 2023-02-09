using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class BankBranchRepository
    {
        private string _connectionString = string.Empty;

        public BankBranchRepository()
            => _connectionString = ConnectionString.Get();
            

        public IEnumerable GetAll()
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
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Query(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public BankBranch GetBy(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Get<BankBranch>(id);
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
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Query(query, new { branchNumber });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Insert(BankBranch bankBranch)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Insert<BankBranch>(bankBranch);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(BankBranch bankBranch)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Update<BankBranch>(bankBranch);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Delete(int id)
        {
            
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var bankBranch = connection.Get<BankBranch>(id);
                    return connection.Delete<BankBranch>(bankBranch);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }

}
