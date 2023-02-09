using System.Collections;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ServiceRepository
    {
        private string _connectionString = string.Empty;

        public ServiceRepository()
            => _connectionString = ConnectionString.Get();


        public IEnumerable GetAll()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.GetAll<Service>(); //OrderBy Tag
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public Service GetBy(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Get<Service>(id);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Insert(Service service)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Insert<Service>(service);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(Service service)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Update<Service>(service);
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
                    var service = connection.Get<Service>(id);
                    return connection.Delete<Service>(service);
                }
                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}