using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class BankBranchRepository
    {
        private string _connectionString = string.Empty;

        public BankBranchRepository()
        {
            _connectionString = ConnectionString.Get();
        }

        
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


        public IEnumerable GetBy(int id)
        {
            string query = @"
                SELECT 
                    * 
                FROM 
                    BankBranch 
                WHERE 
                    Id = @id";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.QueryFirst(query, new { id });
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


        public int Insert(BankBranch bankBranch)
        {
            string insertSQL = @"
                INSERT INTO BankBranch
                    (BranchNumber, Name, Address, Complement, District, City, 
                    Zip, State, ContactName, Phone1, Phone2, Email) 
                VALUES
                    (@BranchNumber, @Name, @Address, @Complement, @District, @City, 
                    @Zip, @State, @ContactName, @Phone1, @Phone2, @Email)";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(insertSQL, bankBranch);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(BankBranch bankBranch)
        {
            string updateSQL = @"
                UPDATE 
                    BankBranch
                SET
                    BranchNumber = @BranchNumber, 
                    Name = @Name, 
                    Address = @Address, 
                    Complement = @Complement, 
                    District = @District, 
                    City = @City, 
                    Zip = @Zip, 
                    State = @State, 
                    ContactName = @ContactName, 
                    Phone1 = @Phone1, 
                    Phone2 = @Phone2, 
                    Email = @Email 
                WHERE 
                    Id = @Id";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, bankBranch);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int Delete(int id)
        {
            string deleteSQL = @"
                DELETE FROM 
                    BankBranch 
                WHERE 
                    Id = @Id";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(deleteSQL, new { id });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }

}
