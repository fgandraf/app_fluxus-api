using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using System.Xml.Linq;

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

                    var sql = new MySqlCommand(@"
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
                            branch_number", 
                        connection);
                    
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var bankBranches = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic branch = new
                            {
                                Id = Convert.ToInt64(dr["id"]),
                                BranchNumber = Convert.ToString(dr["branch_number"]),
                                Name = Convert.ToString(dr["name"]),
                                City = Convert.ToString(dr["city"]),
                                Phone1 = Convert.ToString(dr["phone1"]),
                                Email = Convert.ToString(dr["email"])
                            };
                            bankBranches.Add(branch);
                        }
                        return bankBranches;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }

        public BankBranch GetBy(long id)
        {
            try
            {


                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                        SELECT 
                            * 
                        FROM 
                            bank_branch 
                        WHERE 
                            id = @id", 
                        connection);

                    sql.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var bankBranch = new BankBranch();

                        if (dr.Read())
                        {
                            bankBranch.Id = Convert.ToInt64(dr["id"]);
                            bankBranch.BranchNumber = Convert.ToString(dr["branch_number"]);
                            bankBranch.Name = Convert.ToString(dr["name"]);
                            bankBranch.Address = Convert.ToString(dr["address"]);
                            bankBranch.Complement = Convert.ToString(dr["complement"]);
                            bankBranch.District = Convert.ToString(dr["district"]);
                            bankBranch.City = Convert.ToString(dr["city"]);
                            bankBranch.Zip = Convert.ToString(dr["zip"]);
                            bankBranch.State = Convert.ToString(dr["state"]);
                            bankBranch.Phone1 = Convert.ToString(dr["phone1"]);
                            bankBranch.Phone2 = Convert.ToString(dr["phone2"]);
                            bankBranch.Email = Convert.ToString(dr["email"]);
                        }
                        return bankBranch;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }

        public ArrayList GetNamePhoneEmailBy(string branch_number)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
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
                    
                    sql.Parameters.AddWithValue("@branch_number", branch_number);
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var bankBranches = new ArrayList();

                        if (dr.Read())
                        {
                            dynamic bankBranch = new
                            {
                                BranchNumber = Convert.ToString(dr["branch_number"]),
                                Name = Convert.ToString(dr["name"]),
                                Phone1 = Convert.ToString(dr["phone1"]),
                                Email = Convert.ToString(dr["email"])
                            };
                            bankBranches.Add(bankBranch);
                        }
                        return bankBranches;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }

        public void Insert(BankBranch dado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                        INSERT INTO 
                            bank_branch(
                                branch_number, 
                                name, 
                                address, 
                                complement, 
                                district, 
                                city, 
                                zip, 
                                state, 
                                contact_name, 
                                phone1, 
                                phone2, 
                                email) 
                        VALUES(
                                @branch_number, 
                                @name, 
                                @address, 
                                @complement, 
                                @district, 
                                @city, 
                                @zip, 
                                @state, 
                                @contact_name, 
                                @phone1, 
                                @phone2, 
                                @email)",
                        connection);

                    sql.Parameters.AddWithValue("@branch_number", dado.BranchNumber);
                    sql.Parameters.AddWithValue("@name", dado.Name);
                    sql.Parameters.AddWithValue("@address", dado.Address);
                    sql.Parameters.AddWithValue("@complement", dado.Complement);
                    sql.Parameters.AddWithValue("@district", dado.District);
                    sql.Parameters.AddWithValue("@city", dado.City);
                    sql.Parameters.AddWithValue("@zip", dado.Zip);
                    sql.Parameters.AddWithValue("@state", dado.State);
                    sql.Parameters.AddWithValue("@contact_name", dado.ContactName);
                    sql.Parameters.AddWithValue("@phone1", dado.Phone1);
                    sql.Parameters.AddWithValue("@phone2", dado.Phone2);
                    sql.Parameters.AddWithValue("@email", dado.Email);
                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void Update(long id, BankBranch dado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
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

                    sql.Parameters.AddWithValue("@branch_number", dado.BranchNumber);
                    sql.Parameters.AddWithValue("@name", dado.Name);
                    sql.Parameters.AddWithValue("@address", dado.Address);
                    sql.Parameters.AddWithValue("@complement", dado.Complement);
                    sql.Parameters.AddWithValue("@district", dado.District);
                    sql.Parameters.AddWithValue("@city", dado.City);
                    sql.Parameters.AddWithValue("@zip", dado.Zip);
                    sql.Parameters.AddWithValue("@state", dado.State);
                    sql.Parameters.AddWithValue("@contact_name", dado.ContactName);
                    sql.Parameters.AddWithValue("@phone1", dado.Phone1);
                    sql.Parameters.AddWithValue("@phone2", dado.Phone2);
                    sql.Parameters.AddWithValue("@email", dado.Email);
                    sql.Parameters.AddWithValue("@id", id);
                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sqlSelect = new MySqlCommand(@"
                        SELECT 
                            id 
                        FROM 
                            bank_branch 
                        WHERE 
                            id = @id",
                            connection);

                    sqlSelect.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = sqlSelect.ExecuteReader();

                    if (!dr.HasRows)
                        return false;
                }

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    DELETE FROM 
                        bank_branch 
                    WHERE 
                        id = @id",
                    connection);

                    sql.Parameters.AddWithValue("@id", id);
                    sql.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }

}
