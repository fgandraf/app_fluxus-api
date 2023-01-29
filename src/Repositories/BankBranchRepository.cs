using System;
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

        public ArrayList GetAll()
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
                                id, 
                                branch_number, 
                                name, 
                                city, 
                                phone1, 
                                email 
                            FROM 
                                bank_branch 
                            ORDER BY 
                                branch_number";

                        var reader = command.ExecuteReader();

                        var bankBranches = new ArrayList();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dynamic branch = new
                                {
                                    Id = Convert.ToInt64(reader["id"]),
                                    BranchNumber = Convert.ToString(reader["branch_number"]),
                                    Name = Convert.ToString(reader["name"]),
                                    City = Convert.ToString(reader["city"]),
                                    Phone1 = Convert.ToString(reader["phone1"]),
                                    Email = Convert.ToString(reader["email"])
                                };
                                bankBranches.Add(branch);
                            }
                            return bankBranches;
                        }
                        else
                            return null;
                    }
                }

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
                {
                    connection.Open();

                    using (var command = new MySqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT 
                                * 
                            FROM 
                                bank_branch 
                            WHERE 
                                id = @id";
                        command.Parameters.AddWithValue("@id", id);

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            var bankBranch = new BankBranch();

                            if (reader.Read())
                            {
                                bankBranch.Id = Convert.ToInt64(reader["id"]);
                                bankBranch.BranchNumber = Convert.ToString(reader["branch_number"]);
                                bankBranch.Name = Convert.ToString(reader["name"]);
                                bankBranch.Address = Convert.ToString(reader["address"]);
                                bankBranch.Complement = Convert.ToString(reader["complement"]);
                                bankBranch.District = Convert.ToString(reader["district"]);
                                bankBranch.City = Convert.ToString(reader["city"]);
                                bankBranch.Zip = Convert.ToString(reader["zip"]);
                                bankBranch.State = Convert.ToString(reader["state"]);
                                bankBranch.Phone1 = Convert.ToString(reader["phone1"]);
                                bankBranch.Phone2 = Convert.ToString(reader["phone2"]);
                                bankBranch.Email = Convert.ToString(reader["email"]);
                            }

                            return bankBranch;
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ArrayList GetContacts(string branch_number)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        SELECT 
                            branch_number, 
                            name, 
                            phone1, 
                            email 
                        FROM 
                            bank_branch 
                        WHERE 
                            branch_number = @branch_number",
                        connection);

                    command.Parameters.AddWithValue("@branch_number", branch_number);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var bankBranches = new ArrayList();

                        if (reader.Read())
                        {
                            dynamic bankBranch = new
                            {
                                BranchNumber = Convert.ToString(reader["branch_number"]),
                                Name = Convert.ToString(reader["name"]),
                                Phone1 = Convert.ToString(reader["phone1"]),
                                Email = Convert.ToString(reader["email"])
                            };

                            bankBranches.Add(bankBranch);
                        }
                        return bankBranches;
                    }
                    else
                        return null;
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
