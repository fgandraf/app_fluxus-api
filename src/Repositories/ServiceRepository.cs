using System;
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
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var service = connection.Query(@"
                        SELECT 
                            Id, 
                            Tag, 
                            Description, 
                            ServiceAmount, 
                            MileageAllowance 
                        FROM 
                            Service 
                        ORDER BY 
                            Tag");
                    return service;
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
                    var service = connection.QueryFirst(@"
                        SELECT 
                            * 
                        FROM 
                            Service 
                        WHERE 
                            Id = @id", new { id });
                    return service;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Insert(Service service)
        {
            try
            {
                string insertSQL = @"
                    INSERT INTO Service
                        (Tag, Description, ServiceAmount, MileageAllowance) 
                    VALUES 
                        (@Tag, @Description, @ServiceAmount, @MileageAllowance)";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(insertSQL, service);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(Service service)
        {
            try
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

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(updateSQL, service);
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
                        Service 
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