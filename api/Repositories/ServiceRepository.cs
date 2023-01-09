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

                    var sql = new MySqlCommand(@"
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

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var services = new ArrayList();

                        while (dr.Read())
                        {
                            var service = new Service();

                            service.Id = Convert.ToInt64(dr["id"]);
                            service.Tag = Convert.ToString(dr["tag"]);
                            service.Description = Convert.ToString(dr["description"]);
                            service.ServiceAmount = Convert.ToString(dr["service_amount"]);
                            service.MileageAllowance = Convert.ToString(dr["mileage_allowance"]);

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

                    var sql = new MySqlCommand(@"
                    SELECT 
                        * 
                    FROM 
                        service 
                    WHERE 
                        id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var service = new Service();

                        if (dr.Read())
                        {
                            service.Id = Convert.ToInt64(dr["id"]);
                            service.Tag = Convert.ToString(dr["tag"]);
                            service.Description = Convert.ToString(dr["description"]);
                            service.ServiceAmount = Convert.ToString(dr["service_amount"]);
                            service.MileageAllowance = Convert.ToString(dr["mileage_allowance"]);
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

                    var sql = new MySqlCommand(@"
                    INSERT INTO service
                        (tag, description, service_amount, mileage_allowance) 
                    VALUES 
                        (@tag, @description, @service_amount, @mileage_allowance)",
                        connection);

                    sql.Parameters.AddWithValue("@tag", dado.Tag);
                    sql.Parameters.AddWithValue("@description", dado.Description);
                    sql.Parameters.AddWithValue("@service_amount", dado.ServiceAmount);
                    sql.Parameters.AddWithValue("@mileage_allowance", dado.MileageAllowance);

                    sql.ExecuteNonQuery();

                    return sql.LastInsertedId;
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

                    var sql = new MySqlCommand(@"
                    UPDATE 
                        service 
                    SET 
                        description = @description, 
                        service_amount = @service_amount, 
                        mileage_allowance = @mileage_allowance 
                    WHERE 
                        id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@id", id);
                    sql.Parameters.AddWithValue("@description", dado.Description);
                    sql.Parameters.AddWithValue("@service_amount", dado.ServiceAmount);
                    sql.Parameters.AddWithValue("@mileage_allowance", dado.MileageAllowance);

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
                            service 
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
                        service 
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