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
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var bankBranches = connection.Query(@"
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
                            BranchNumber");
                    return bankBranches;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetBy(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var bankBranch = connection.Query(@"
                        SELECT 
                            * 
                        FROM 
                            BankBranch 
                        WHERE 
                            Id = @id", new { id });
                    return bankBranch;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetContacts(string branchNumber)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var bankBranch = connection.Query(@"
                        SELECT 
                            BranchNumber, 
                            Name, 
                            Phone1, 
                            Email 
                        FROM 
                            BankBranch 
                        WHERE 
                            BranchNumber = @branchNumber", new { branchNumber });
                    return bankBranch;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Insert(BankBranch bankBranch)
        {
            try
            {
                string insertSQL = @"
                    INSERT INTO BankBranch
                        (BranchNumber, Name, Address, Complement, District, City, 
                        Zip, State, ContactName, Phone1, Phone2, Email) 
                    VALUES
                        (@BranchNumber, @Name, @Address, @Complement, @District, @City, 
                        @Zip, @State, @ContactName, @Phone1, @Phone2, @Email)";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(insertSQL, bankBranch);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(BankBranch bankBranch)
        {
            try
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

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(updateSQL, bankBranch);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int Delete(int id)
        {
            try
            {
                string deleteSQL = @"
                    DELETE FROM 
                        BankBranch 
                    WHERE 
                        Id = @Id";

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
