using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

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
            string query = @"
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
                    Tag";

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


        public IEnumerable GetTagNameid()
        {
            string query = @"
                SELECT 
                    Id,
                    Tag, 
                    Nameid 
                FROM 
                    Professional 
                ORDER BY 
                    Tag";

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


        public IEnumerable GetUserInfoBy(string userName)
        {
            string query = @"
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
                    UserName = @userName";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.QueryFirst(query, new { userName });
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
                    Professional 
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


        public int Insert(Professional professional)
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

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(insertSQL, professional);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(Professional professional)
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

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, professional);
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
                    Professional 
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
