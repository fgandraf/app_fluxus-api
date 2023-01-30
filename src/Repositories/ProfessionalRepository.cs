using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Azure.Identity;

namespace FluxusApi.Repositories
{

    public class ProfessionalRepository
    {
        private string _connectionString = string.Empty;
        public ProfessionalRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public IEnumerable GetAll()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var professionals = connection.Query(@"
                        SELECT 
                            Id, 
                            Tag, 
                            Name, 
                            Profession, 
                            Phone1, 
                            UserActive
                        FROM 
                            Professional 
                        ORDER BY 
                            Tag");
                    return professionals;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetTagNameid()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var professionals = connection.Query(@"
                        SELECT 
                            Id,
                            Tag, 
                            Nameid 
                        FROM 
                            Professional 
                        ORDER BY 
                            Tag");
                    return professionals;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetUserInfoBy(string userName)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var professional = connection.QueryFirst(@"
                        SELECT 
                            Id, 
                            TechnicianResponsible, 
                            LegalResponsible, 
                            UserName, 
                            UserPassword, 
                            UserActive 
                        FROM 
                            Professional 
                        WHERE 
                            UserName = @userName", new { userName });
                        return professional;
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
                    var professional = connection.QueryFirst(@"
                        SELECT 
                            * 
                        FROM 
                            Professional 
                        WHERE 
                            Id = @id", new { id });
                    return professional;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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
