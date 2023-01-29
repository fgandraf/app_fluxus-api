using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{

    public class ProfessionalRepository
    {
        private string _connectionString = string.Empty;
        public ProfessionalRepository()
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
                    
                    var command = new MySqlCommand(@"
                        SELECT 
                            id, 
                            tag, 
                            name, 
                            profession, 
                            phone1, 
                            user_active 
                        FROM 
                            professional 
                        ORDER BY 
                            tag", 
                        connection);
                    
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var professionals = new ArrayList();

                        while (reader.Read())
                        {
                            Professional professional = new Professional();

                            professional.Id = Convert.ToInt64(reader["id"]);
                            professional.Tag = Convert.ToString(reader["tag"]);
                            professional.Name = Convert.ToString(reader["name"]);
                            professional.Profession = Convert.ToString(reader["profession"]);
                            professional.Phone1 = Convert.ToString(reader["phone1"]);
                            professional.UserActive = Convert.ToBoolean(reader["user_active"]);

                            professionals.Add(professional);
                        }

                        return professionals;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public ArrayList GetTagNameid()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var command = new MySqlCommand(@"
                        SELECT 
                            id,
                            tag, 
                            nameid 
                        FROM 
                            professional 
                        ORDER BY 
                            tag", 
                        connection);
                    
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var professionals = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic professional = new
                            {
                                Id = Convert.ToString(reader["id"]),
                                Tag = Convert.ToString(reader["tag"]),
                                NameId = Convert.ToString(reader["nameid"])
                            };

                            professionals.Add(professional);
                        }
                        return professionals;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return null;
        }


        public ArrayList GetUserInfoBy(string userName)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var command = new MySqlCommand(@"
                        SELECT 
                            id, 
                            technician_responsible, 
                            legal_responsible, 
                            user_name, 
                            user_password, 
                            user_active 
                        FROM 
                            professional 
                        WHERE 
                            user_name = @user_name", 
                        connection);
                    
                    command.Parameters.AddWithValue("@user_name", userName);
                    
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var users = new ArrayList();

                        if (reader.Read())
                        {
                            dynamic user = new
                            {
                                Id = Convert.ToString(reader["id"]),
                                TechnicianResponsible = Convert.ToBoolean(reader["technician_responsible"]),
                                LegalResponsible = Convert.ToBoolean(reader["legal_responsible"]),
                                UserActive = Convert.ToBoolean(reader["user_active"]),
                                UserName = Convert.ToString(reader["user_name"]),
                                UserPassword = Convert.ToString(reader["user_password"])
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return null;
        }


        public Professional GetBy(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var command = new MySqlCommand(@"
                        SELECT 
                            * 
                        FROM 
                            professional 
                        WHERE 
                            id = @id", 
                        connection);
                    
                    command.Parameters.AddWithValue("@id", id);
                    
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Professional professional = new Professional();
                        if (reader.Read())
                        {
                            professional.Id = Convert.ToInt64(reader["id"]);
                            professional.Tag = Convert.ToString(reader["tag"]);
                            professional.Name = Convert.ToString(reader["name"]);
                            professional.NameId = Convert.ToString(reader["nameid"]);
                            professional.Cpf = Convert.ToString(reader["cpf"]);
                            professional.Birthday = Convert.ToString(reader["birthday"]);
                            professional.Profession = Convert.ToString(reader["profession"]);
                            professional.PermitNumber = Convert.ToString(reader["permit_number"]);
                            professional.Association = Convert.ToString(reader["association"]);
                            professional.Phone1 = Convert.ToString(reader["phone1"]);
                            professional.Phone2 = Convert.ToString(reader["phone2"]);
                            professional.Email = Convert.ToString(reader["email"]);
                            professional.TechnicianResponsible = Convert.ToBoolean(reader["technician_responsible"]);
                            professional.LegalResponsible = Convert.ToBoolean(reader["legal_responsible"]);
                            professional.UserActive = Convert.ToBoolean(reader["user_active"]);
                            professional.UserName = Convert.ToString(reader["user_name"]);
                            professional.UserPassword = Convert.ToString(reader["user_password"]);
                        }
                        return professional;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return null;
        }


        public long Insert(Professional professional)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var command = new MySqlCommand(@"
                        INSERT INTO professional
                            (tag, name, nameid, cpf, birthday, profession, permit_number, 
                            association, phone1, phone2, email, technician_responsible, 
                            legal_responsible, user_active, user_name, user_password) 
                        VALUES
                            (@tag, @name, @nameid, @cpf, @birthday, @profession, @permit_number, 
                            @association, @phone1, @phone2, @email, @technician_responsible, 
                            @legal_responsible, @user_active, @user_name, @user_password)", 
                        connection);
                    
                    command.Parameters.AddWithValue("@tag", professional.Tag);
                    command.Parameters.AddWithValue("@name", professional.Name);
                    command.Parameters.AddWithValue("@nameid", professional.NameId);
                    command.Parameters.AddWithValue("@cpf", professional.Cpf);
                    command.Parameters.AddWithValue("@birthday", professional.Birthday);
                    command.Parameters.AddWithValue("@profession", professional.Profession);
                    command.Parameters.AddWithValue("@permit_number", professional.PermitNumber);
                    command.Parameters.AddWithValue("@association", professional.Association);
                    command.Parameters.AddWithValue("@phone1", professional.Phone1);
                    command.Parameters.AddWithValue("@phone2", professional.Phone2);
                    command.Parameters.AddWithValue("@email", professional.Email);
                    command.Parameters.AddWithValue("@technician_responsible", professional.TechnicianResponsible);
                    command.Parameters.AddWithValue("@legal_responsible", professional.LegalResponsible);
                    command.Parameters.AddWithValue("@user_active", professional.UserActive);
                    command.Parameters.AddWithValue("@user_name", professional.UserName);
                    command.Parameters.AddWithValue("@user_password", professional.UserPassword);

                    command.ExecuteNonQuery();
                    return command.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(Professional professional)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var command = new MySqlCommand(@"
                        UPDATE 
                            professional 
                        SET 
                            name = @name, 
                            nameid = @nameid, 
                            cpf = @cpf, 
                            birthday = @birthday, 
                            profession = @profession, 
                            permit_number = @permit_number, 
                            association = @association, 
                            phone1 = @phone1, 
                            phone2 = @phone2, 
                            email = @email, 
                            technician_responsible = @technician_responsible, 
                            legal_responsible = @legal_responsible, 
                            user_active = @user_active, 
                            user_name = @user_name, 
                            user_password = @user_password 
                        WHERE 
                            id = @id", 
                        connection);
                    
                    command.Parameters.AddWithValue("@id", professional.Id);
                    command.Parameters.AddWithValue("@name", professional.Name);
                    command.Parameters.AddWithValue("@nameid", professional.NameId);
                    command.Parameters.AddWithValue("@cpf", professional.Cpf);
                    command.Parameters.AddWithValue("@birthday", professional.Birthday);
                    command.Parameters.AddWithValue("@profession", professional.Profession);
                    command.Parameters.AddWithValue("@permit_number", professional.PermitNumber);
                    command.Parameters.AddWithValue("@association", professional.Association);
                    command.Parameters.AddWithValue("@phone1", professional.Phone1);
                    command.Parameters.AddWithValue("@phone2", professional.Phone2);
                    command.Parameters.AddWithValue("@email", professional.Email);
                    command.Parameters.AddWithValue("@technician_responsible", professional.TechnicianResponsible);
                    command.Parameters.AddWithValue("@legal_responsible", professional.LegalResponsible);
                    command.Parameters.AddWithValue("@user_active", professional.UserActive);
                    command.Parameters.AddWithValue("@user_name", professional.UserName);
                    command.Parameters.AddWithValue("@user_password", professional.UserPassword);
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
                            professional 
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
                                professional 
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
