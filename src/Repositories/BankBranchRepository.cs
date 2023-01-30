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


        public void Insert(BankBranch bankBranch)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        INSERT INTO bank_branch
                            (branch_number, name, address, complement,district,city, 
                            zip, state, contact_name, phone1, phone2, email) 
                        VALUES
                            (@branch_number, @name, @address, @complement, @district, @city, 
                            @zip, @state, @contact_name, @phone1, @phone2, @email)",
                        connection);

                    command.Parameters.AddWithValue("@branch_number", bankBranch.BranchNumber);
                    command.Parameters.AddWithValue("@name", bankBranch.Name);
                    command.Parameters.AddWithValue("@address", bankBranch.Address);
                    command.Parameters.AddWithValue("@complement", bankBranch.Complement);
                    command.Parameters.AddWithValue("@district", bankBranch.District);
                    command.Parameters.AddWithValue("@city", bankBranch.City);
                    command.Parameters.AddWithValue("@zip", bankBranch.Zip);
                    command.Parameters.AddWithValue("@state", bankBranch.State);
                    command.Parameters.AddWithValue("@contact_name", bankBranch.ContactName);
                    command.Parameters.AddWithValue("@phone1", bankBranch.Phone1);
                    command.Parameters.AddWithValue("@phone2", bankBranch.Phone2);
                    command.Parameters.AddWithValue("@email", bankBranch.Email);
                    command.ExecuteNonQuery();
                }
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
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        UPDATE 
                            bank_branch
                        SET
                            branch_number = @branch_number, 
                            name = @name, 
                            address = @address, 
                            complement = @complement, 
                            district = @district, 
                            city = @city, 
                            zip = @zip, 
                            state = @state, 
                            contact_name = @contact_name, 
                            phone1 = @phone1, 
                            phone2 = @phone2, 
                            email = @email 
                        WHERE 
                            id = @id",
                        connection);

                    command.Parameters.AddWithValue("@branch_number", bankBranch.BranchNumber);
                    command.Parameters.AddWithValue("@name", bankBranch.Name);
                    command.Parameters.AddWithValue("@address", bankBranch.Address);
                    command.Parameters.AddWithValue("@complement", bankBranch.Complement);
                    command.Parameters.AddWithValue("@district", bankBranch.District);
                    command.Parameters.AddWithValue("@city", bankBranch.City);
                    command.Parameters.AddWithValue("@zip", bankBranch.Zip);
                    command.Parameters.AddWithValue("@state", bankBranch.State);
                    command.Parameters.AddWithValue("@contact_name", bankBranch.ContactName);
                    command.Parameters.AddWithValue("@phone1", bankBranch.Phone1);
                    command.Parameters.AddWithValue("@phone2", bankBranch.Phone2);
                    command.Parameters.AddWithValue("@email", bankBranch.Email);
                    command.Parameters.AddWithValue("@id", bankBranch.Id);
                    command.ExecuteNonQuery();
                }
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
                    connection.Open();

                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT 
                                id 
                            FROM 
                                bank_branch 
                            WHERE 
                                id = @id";
                        command.Parameters.AddWithValue("@id", id);
                        
                        var reader = command.ExecuteReader();
                        if (!reader.HasRows)
                            return false;
                    }
                }


                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            DELETE FROM 
                                bank_branch 
                            WHERE 
                                id = @id";
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }

}
