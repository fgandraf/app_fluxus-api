using System;
using System.Collections;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;

namespace FluxusApi.Repositories
{
    public class ServiceRepository
    {
        private string _connectionString = string.Empty;

        public ServiceRepository()
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
                            description, 
                            service_amount, 
                            mileage_allowance 
                        FROM 
                            service 
                        ORDER BY 
                            tag",
                            connection);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var services = new ArrayList();

                        while (reader.Read())
                        {
                            var service = new Service();

                            service.Id = Convert.ToInt64(reader["id"]);
                            service.Tag = Convert.ToString(reader["tag"]);
                            service.Description = Convert.ToString(reader["description"]);
                            service.ServiceAmount = Convert.ToString(reader["service_amount"]);
                            service.MileageAllowance = Convert.ToString(reader["mileage_allowance"]);

                            services.Add(service);
                        }

                        return services;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public Service GetBy(long id)
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
                            service 
                        WHERE 
                            id = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var service = new Service();

                        if (reader.Read())
                        {
                            service.Id = Convert.ToInt64(reader["id"]);
                            service.Tag = Convert.ToString(reader["tag"]);
                            service.Description = Convert.ToString(reader["description"]);
                            service.ServiceAmount = Convert.ToString(reader["service_amount"]);
                            service.MileageAllowance = Convert.ToString(reader["mileage_allowance"]);
                        }

                        return service;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public long Insert(Service dado)
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

                    reader.Parameters.AddWithValue("@tag", dado.Tag);
                    reader.Parameters.AddWithValue("@description", dado.Description);
                    reader.Parameters.AddWithValue("@service_amount", dado.ServiceAmount);
                    reader.Parameters.AddWithValue("@mileage_allowance", dado.MileageAllowance);

                    reader.ExecuteNonQuery();

                    return reader.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(long id, Service dado)
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

                    reader.Parameters.AddWithValue("@id", id);
                    reader.Parameters.AddWithValue("@description", dado.Description);
                    reader.Parameters.AddWithValue("@service_amount", dado.ServiceAmount);
                    reader.Parameters.AddWithValue("@mileage_allowance", dado.MileageAllowance);

                    reader.ExecuteNonQuery();
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