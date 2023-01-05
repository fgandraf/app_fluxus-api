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
                    
                    var sql = new MySqlCommand(@"
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
                    
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var professionals = new ArrayList();

                        while (dr.Read())
                        {
                            Professional professional = new Professional();

                            professional.Id = Convert.ToInt64(dr["id"]);
                            professional.Tag = Convert.ToString(dr["tag"]);
                            professional.Name = Convert.ToString(dr["name"]);
                            professional.Profession = Convert.ToString(dr["profession"]);
                            professional.Phone1 = Convert.ToString(dr["phone1"]);
                            professional.UserActive = Convert.ToBoolean(dr["user_active"]);

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


        public ArrayList GetCodigoENomeid()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var sql = new MySqlCommand(@"
                        SELECT 
                            tag, 
                            nameid 
                        FROM 
                            professional 
                        ORDER BY 
                            tag", 
                        connection);
                    
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var professionals = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic professional = new
                            {
                                Tag = Convert.ToString(dr["tag"]),
                                NameId = Convert.ToString(dr["nameid"])
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
                    
                    var sql = new MySqlCommand(@"
                        SELECT 
                            tag, 
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
                    
                    sql.Parameters.AddWithValue("@user_name", userName);
                    
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var users = new ArrayList();

                        if (dr.Read())
                        {
                            dynamic user = new
                            {
                                Tag = Convert.ToString(dr["tag"]),
                                TechnicianResponsible = Convert.ToBoolean(dr["technician_responsible"]),
                                LegalResponsible = Convert.ToBoolean(dr["legal_responsible"]),
                                UserActive = Convert.ToBoolean(dr["user_active"]),
                                UserName = Convert.ToString(dr["user_name"]),
                                UserPassword = Convert.ToString(dr["user_password"])
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


        public Professional GetBy(long id)
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
                            professional 
                        WHERE 
                            id = @id", 
                        connection);
                    
                    sql.Parameters.AddWithValue("@id", id);
                    
                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Professional professional = new Professional();
                        if (dr.Read())
                        {
                            professional.Id = Convert.ToInt64(dr["id"]);
                            professional.Tag = Convert.ToString(dr["tag"]);
                            professional.Name = Convert.ToString(dr["name"]);
                            professional.NameId = Convert.ToString(dr["nameid"]);
                            professional.Cpf = Convert.ToString(dr["cpf"]);
                            professional.Birthday = Convert.ToString(dr["birthday"]);
                            professional.Profession = Convert.ToString(dr["profession"]);
                            professional.PermitNumber = Convert.ToString(dr["permit_number"]);
                            professional.Association = Convert.ToString(dr["association"]);
                            professional.Phone1 = Convert.ToString(dr["phone1"]);
                            professional.Phone2 = Convert.ToString(dr["phone2"]);
                            professional.Email = Convert.ToString(dr["email"]);
                            professional.TechnicianResponsible = Convert.ToBoolean(dr["technician_responsible"]);
                            professional.LegalResponsible = Convert.ToBoolean(dr["legal_responsible"]);
                            professional.UserActive = Convert.ToBoolean(dr["user_active"]);
                            professional.UserName = Convert.ToString(dr["user_name"]);
                            professional.UserPassword = Convert.ToString(dr["user_password"]);
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


        public long Insert(Professional pro)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var sql = new MySqlCommand(@"
                        INSERT INTO 
                            professional(
                                tag, 
                                name, 
                                nameid, 
                                cpf, 
                                birthday, 
                                profession, 
                                permit_number, 
                                association, 
                                phone1, 
                                phone2, 
                                email, 
                                technician_responsible, 
                                legal_responsible, 
                                user_active, 
                                user_name, 
                                user_password) 
                        VALUES (
                            @tag, 
                            @name, 
                            @nameid, 
                            @cpf, 
                            @birthday, 
                            @profession, 
                            @permit_number, 
                            @association, 
                            @phone1, 
                            @phone2, 
                            @email, 
                            @technician_responsible, 
                            @legal_responsible, 
                            @user_active, 
                            @user_name, 
                            @user_password)", 
                        connection);
                    
                    sql.Parameters.AddWithValue("@tag", pro.Tag);
                    sql.Parameters.AddWithValue("@name", pro.Name);
                    sql.Parameters.AddWithValue("@nameid", pro.NameId);
                    sql.Parameters.AddWithValue("@cpf", pro.Cpf);
                    sql.Parameters.AddWithValue("@birthday", Util.DateOrNull(pro.Birthday));
                    sql.Parameters.AddWithValue("@profession", pro.Profession);
                    sql.Parameters.AddWithValue("@permit_number", pro.PermitNumber);
                    sql.Parameters.AddWithValue("@association", pro.Association);
                    sql.Parameters.AddWithValue("@phone1", pro.Phone1);
                    sql.Parameters.AddWithValue("@phone2", pro.Phone2);
                    sql.Parameters.AddWithValue("@email", pro.Email);
                    sql.Parameters.AddWithValue("@technician_responsible", pro.TechnicianResponsible);
                    sql.Parameters.AddWithValue("@legal_responsible", pro.LegalResponsible);
                    sql.Parameters.AddWithValue("@user_active", pro.UserActive);
                    sql.Parameters.AddWithValue("@user_name", pro.UserName);
                    sql.Parameters.AddWithValue("@user_password", pro.UserPassword);

                    sql.ExecuteNonQuery();
                    return sql.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(long id, Professional pro)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var sql = new MySqlCommand(@"
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
                    
                    sql.Parameters.AddWithValue("@id", id);
                    sql.Parameters.AddWithValue("@name", pro.Name);
                    sql.Parameters.AddWithValue("@nameid", pro.NameId);
                    sql.Parameters.AddWithValue("@cpf", pro.Cpf);
                    sql.Parameters.AddWithValue("@birthday", Util.DateOrNull(pro.Birthday));
                    sql.Parameters.AddWithValue("@profession", pro.Profession);
                    sql.Parameters.AddWithValue("@permit_number", pro.PermitNumber);
                    sql.Parameters.AddWithValue("@association", pro.Association);
                    sql.Parameters.AddWithValue("@phone1", pro.Phone1);
                    sql.Parameters.AddWithValue("@phone2", pro.Phone2);
                    sql.Parameters.AddWithValue("@email", pro.Email);
                    sql.Parameters.AddWithValue("@technician_responsible", pro.TechnicianResponsible);
                    sql.Parameters.AddWithValue("@legal_responsible", pro.LegalResponsible);
                    sql.Parameters.AddWithValue("@user_active", pro.UserActive);
                    sql.Parameters.AddWithValue("@user_name", pro.UserName);
                    sql.Parameters.AddWithValue("@user_password", pro.UserPassword);
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
                            professional 
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
                            professional 
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
