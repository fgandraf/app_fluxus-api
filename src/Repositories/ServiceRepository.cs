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


        public long Insert(Service service)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var reader = new MySqlCommand(@"
                        INSERT INTO service
                            (tag, description, service_amount, mileage_allowance) 
                        VALUES 
                            (@tag, @description, @service_amount, @mileage_allowance)",
                            connection);

                    reader.Parameters.AddWithValue("@tag", service.Tag);
                    reader.Parameters.AddWithValue("@description", service.Description);
                    reader.Parameters.AddWithValue("@service_amount", service.ServiceAmount);
                    reader.Parameters.AddWithValue("@mileage_allowance", service.MileageAllowance);

                    reader.ExecuteNonQuery();

                    return reader.LastInsertedId;
                }
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
                {
                    connection.Open();

                    var reader = new MySqlCommand(@"
                        UPDATE 
                            service 
                        SET 
                            description = @description, 
                            service_amount = @service_amount, 
                            mileage_allowance = @mileage_allowance 
                        WHERE 
                            id = @id",
                            connection);

                    reader.Parameters.AddWithValue("@id", service.Id);
                    reader.Parameters.AddWithValue("@description", service.Description);
                    reader.Parameters.AddWithValue("@service_amount", service.ServiceAmount);
                    reader.Parameters.AddWithValue("@mileage_allowance", service.MileageAllowance);

                    reader.ExecuteNonQuery();
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
                                service 
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
                                service 
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