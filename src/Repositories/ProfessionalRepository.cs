using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ProfessionalRepository
    {
        private string _connectionString = string.Empty;

        public ProfessionalRepository()
            => _connectionString = ConnectionString.Get();


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
                    return connection.Query(query, new { userName });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public Professional GetBy(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Get<Professional>(id);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Insert(Professional professional)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Insert<Professional>(professional);
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
                    connection.Update<Professional>(professional);
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
                    var professional = connection.Get<Professional>(id);
                    return connection.Delete<Professional>(professional);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }

}
