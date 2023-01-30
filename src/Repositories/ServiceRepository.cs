using System.Collections;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using Dapper;

namespace FluxusApi.Repositories
{
    public class ServiceRepository
    {
        private string _connectionString = string.Empty;

        public ServiceRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public IEnumerable GetAll()
        {
            string query = @"
                SELECT 
                    Id, 
                    Tag, 
                    Description, 
                    ServiceAmount, 
                    MileageAllowance 
                FROM 
                    Service 
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


        public IEnumerable GetBy(int id)
        {
            string query = @"
                SELECT 
                    * 
                FROM 
                    Service 
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


        public int Insert(Service service)
        {
            string insertSQL = @"
                INSERT INTO Service
                    (Tag, Description, ServiceAmount, MileageAllowance) 
                VALUES 
                    (@Tag, @Description, @ServiceAmount, @MileageAllowance)";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(insertSQL, service);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(Service service)
        {
            string updateSQL = @"
                UPDATE 
                    Service 
                SET 
                    Description = @Description, 
                    ServiceAmount = @ServiceAmount, 
                    MileageAllowance = @MileageAllowance 
                WHERE 
                    Id = @Id";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, service);
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
                    Service 
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