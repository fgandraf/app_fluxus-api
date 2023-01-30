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


        public int Insert(Professional professional)
        {
            try
            {
                string insertSQL = @"
                    INSERT INTO Professional
                        (Tag, Name, Nameid, Cpf, Birthday, Profession, PermitNumber, 
                        Association, Phone1, Phone2, Email, TechnicianResponsible, 
                        LegalResponsible, UserActive, UserName, UserPassword) 
                    VALUES
                        (@Tag, @Name, @Nameid, @Cpf, @Birthday, @Profession, @PermitNumber, 
                        @Association, @Phone1, @Phone2, @Email, @TechnicianResponsible, 
                        @LegalResponsible, @UserActive, @UserName, @UserPassword)";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(insertSQL, professional);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(Professional professional)
        {
            try
            {
                string updateSQL = @"
                    UPDATE 
                        Professional 
                    SET 
                        Name = @Name, 
                        Nameid = @Nameid, 
                        Cpf = @Cpf, 
                        Birthday = @Birthday, 
                        Profession = @Profession, 
                        PermitNumber = @PermitNumber, 
                        Association = @Association, 
                        Phone1 = @Phone1, 
                        Phone2 = @Phone2, 
                        Email = @Email, 
                        TechnicianResponsible = @TechnicianResponsible, 
                        LegalResponsible = @LegalResponsible, 
                        UserActive = @UserActive, 
                        UserName = @UserName, 
                        UserPassword = @UserPassword 
                    WHERE 
                        Id = @Id";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(updateSQL, professional);
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
                        Professional 
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
