using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Components.Routing;

namespace FluxusApi.Repositories
{


    public class ServiceOrderRepository
    {
        private string _connectionString = string.Empty;

        public ServiceOrderRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public ArrayList GetOrdersFlow()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    SELECT 
                        id, 
                        reference_code, 
                        title, 
                        status, 
                        professional_id 
                    FROM 
                        service_order 
                    WHERE 
                        invoice_id = 0 
                    ORDER BY 
                        order_date",
                        connection);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var orders = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(dr["id"]),
                                ReferenceCode = Convert.ToString(dr["reference_code"]),
                                Titulo = Convert.ToString(dr["title"]),
                                Status = Convert.ToString(dr["status"]),
                                ProfessionalId = Convert.ToString(dr["professional_id"])
                            };

                            orders.Add(order);
                        }

                        return orders;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public ArrayList GetInvoiced(long invoice_id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    SELECT 
                        id, order_date, reference_code, professional_id, service_id, 
                        city, customer_name, survey_date, done_date, invoice_id, 
                        status, service_amount, mileage_allowance 
                    FROM 
                        service_order 
                    WHERE 
                        invoice_id = @invoice_id 
                    ORDER BY 
                        done_date",
                        connection);

                    sql.Parameters.AddWithValue("@invoice_id", invoice_id);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var orders = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(dr["id"]),
                                OrderDate = Convert.ToDateTime(dr["order_date"]),
                                ReferenceCode = Convert.ToString(dr["reference_code"]),
                                ProfessionalId = Convert.ToString(dr["professional_id"]),
                                ServiceId = Convert.ToString(dr["service_id"]),
                                City = Convert.ToString(dr["city"]),
                                CustomerName = Convert.ToString(dr["customer_name"]),
                                SurveyDate = Convert.ToDateTime(dr["survey_date"]),
                                DoneDate = Convert.ToDateTime(dr["done_date"]),
                                Fatura_cod = Convert.ToInt64(dr["invoice_id"]),
                                Status = Convert.ToString(dr["status"]),
                                ServiceAmount = Convert.ToDouble(dr["service_amount"]),
                                MileageAllowance = Convert.ToDouble(dr["mileage_allowance"])
                            };
                            orders.Add(order);
                        }

                        return orders;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public ArrayList GetDoneToInvoice()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    SELECT 
                        id, order_date, reference_code, professional_id, service_id, city, 
                        customer_name, survey_date, done_date, service_amount, mileage_allowance 
                    FROM 
                        service_order 
                    WHERE 
                        invoice_id = 0 
                    AND 
                        status = 'CONCLUÍDA' 
                    ORDER BY 
                        done_date",
                        connection);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var orders = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(dr["id"]),
                                OrderDate = Convert.ToDateTime(dr["order_date"]),
                                ReferenceCode = Convert.ToString(dr["reference_code"]),
                                ProfessionalId = Convert.ToString(dr["professional_id"]),
                                ServiceId = Convert.ToString(dr["service_id"]),
                                City = Convert.ToString(dr["city"]),
                                CustomerName = Convert.ToString(dr["customer_name"]),
                                SurveyDate = Convert.ToDateTime(dr["survey_date"]),
                                DoneDate = Convert.ToDateTime(dr["done_date"]),
                                ServiceAmount = Convert.ToDouble(dr["service_amount"]),
                                MileageAllowance = Convert.ToDouble(dr["mileage_allowance"])
                            };
                            orders.Add(order);
                        }

                        return orders;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }

        public ArrayList GetFiltered(string filtro)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(filtro, connection);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var orders = new ArrayList();

                        while (dr.Read())
                        {
                            ServiceOrder order = new ServiceOrder();

                            order.Id = Convert.ToInt64(dr["id"]);
                            order.ReferenceCode = Convert.ToString(dr["reference_code"]);
                            order.Branch = Convert.ToString(dr["branch"]);
                            order.Title = Convert.ToString(dr["title"]);
                            order.OrderDate = Util.DateTimeToShortDateString(Convert.ToString(dr["order_date"]));
                            order.Deadline = Convert.ToDateTime(dr["deadline"]);
                            order.ProfessionalId = Convert.ToString(dr["professional_id"]);
                            order.ServiceId = Convert.ToString(dr["service_id"]);
                            order.ServiceAmount = Convert.ToString(dr["service_amount"]);
                            order.MileageAllowance = Convert.ToString(dr["mileage_allowance"]);
                            order.Siopi = Convert.ToBoolean(dr["siopi"]);
                            order.CustomerName = Convert.ToString(dr["customer_name"]);
                            order.City = Convert.ToString(dr["city"]);
                            order.ContactName = Convert.ToString(dr["contact_name"]);
                            order.ContactPhone = Convert.ToString(dr["contact_phone"]);
                            order.Coordinates = Convert.ToString(dr["coordinates"]);
                            order.Status = Convert.ToString(dr["status"]);
                            order.PendingDate = Util.DateTimeToShortDateString(Convert.ToString(dr["pending_date"]));
                            order.SurveyDate = Util.DateTimeToShortDateString(Convert.ToString(dr["survey_date"]));
                            order.DoneDate = Util.DateTimeToShortDateString(Convert.ToString(dr["done_date"]));
                            order.Comments = Convert.ToString(dr["comments"]);
                            order.InvoiceId = Convert.ToInt64(dr["invoice_id"]);
                            orders.Add(order);
                        }

                        return orders;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public ArrayList GetProfessionals(long invoice_id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    SELECT DISTINCT 
                        t1.professional_id, 
                        t2.nameid 
                    FROM 
                        service_order t1 
                    INNER JOIN 
                        professional t2 
                    on 
                        t1.professional_id = t2.id 
                    WHERE 
                        t1.invoice_id = @invoice_id 
                    ORDER BY 
                        t2.nameid",
                        connection);

                    sql.Parameters.AddWithValue("@invoice_id", invoice_id);

                    MySqlDataReader dr = sql.ExecuteReader();


                    if (dr.HasRows)
                    {
                        var professionals = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic professional = new
                            {
                                ProfessionalId = Convert.ToString(dr["professional_id"]),
                                Nameid = Convert.ToString(dr["nameid"])
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


        public ArrayList GetOrderedCities()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    SELECT DISTINCT 
                        city 
                    FROM 
                        service_order 
                    ORDER BY 
                        city",
                        connection);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var cities = new ArrayList();

                        while (dr.Read())
                        {
                            dynamic city = new
                            {
                                City = Convert.ToString(dr["city"])
                            };
                            cities.Add(city);
                        }

                        return cities;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public ServiceOrder GetBy(long id)
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
                        service_order 
                    WHERE 
                        id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@id", id);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var order = new ServiceOrder();

                        if (dr.Read())
                        {
                            order.Id = Convert.ToInt64(dr["id"]);
                            order.ReferenceCode = Convert.ToString(dr["reference_code"]);
                            order.Branch = Convert.ToString(dr["branch"]);
                            order.Title = Convert.ToString(dr["title"]);
                            order.OrderDate = Convert.ToString(dr["order_date"]);
                            order.Deadline = Convert.ToDateTime(dr["deadline"]);
                            order.ProfessionalId = Convert.ToString(dr["professional_id"]);
                            order.ServiceId = Convert.ToString(dr["service_id"]);
                            order.ServiceAmount = Convert.ToString(dr["service_amount"]);
                            order.MileageAllowance = Convert.ToString(dr["mileage_allowance"]);
                            order.Siopi = Convert.ToBoolean(dr["siopi"]);
                            order.CustomerName = Convert.ToString(dr["customer_name"]);
                            order.City = Convert.ToString(dr["city"]);
                            order.ContactName = Convert.ToString(dr["contact_name"]);
                            order.ContactPhone = Convert.ToString(dr["contact_phone"]);
                            order.Coordinates = Convert.ToString(dr["coordinates"]);
                            order.Status = Convert.ToString(dr["status"]);
                            order.PendingDate = Convert.ToString(dr["pending_date"]);
                            order.SurveyDate = Convert.ToString(dr["survey_date"]);
                            order.DoneDate = Convert.ToString(dr["done_date"]);
                            order.Comments = Convert.ToString(dr["comments"]);
                            order.InvoiceId = Convert.ToInt64(dr["invoice_id"]);
                        }

                        if (order.OrderDate == "01/01/0001 00:00:00")
                            order.OrderDate = string.Empty;

                        if (order.PendingDate == "01/01/0001 00:00:00")
                            order.PendingDate = string.Empty;

                        if (order.SurveyDate == "01/01/0001 00:00:00")
                            order.SurveyDate = string.Empty;

                        if (order.DoneDate == "01/01/0001 00:00:00")
                            order.DoneDate = string.Empty;

                        return order;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public long Insert(ServiceOrder dado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    INSERT INTO service_order
                        (title, reference_code, branch, order_date, deadline, professional_id, service_id, 
                        service_amount, mileage_allowance, siopi, customer_name, city, contact_name, 
                        contact_phone, coordinates, status, pending_date, survey_date, done_date, comments) 
                    VALUES 
                        (@title, @reference_code, @branch, @order_date, @deadline, @professional_id, @service_id, 
                        @service_amount, @mileage_allowance, @siopi, @customer_name, @city, @contact_name, 
                        @contact_phone, @coordinates, @status, @pending_date, @survey_date, @done_date, @comments)",
                        connection);

                    sql.Parameters.AddWithValue("@title", dado.Title);
                    sql.Parameters.AddWithValue("@reference_code", dado.ReferenceCode);
                    sql.Parameters.AddWithValue("@branch", dado.Branch);
                    sql.Parameters.AddWithValue("@order_date", Util.DateOrNull(dado.OrderDate));
                    sql.Parameters.AddWithValue("@deadline", dado.Deadline);
                    sql.Parameters.AddWithValue("@professional_id", dado.ProfessionalId);
                    sql.Parameters.AddWithValue("@service_id", dado.ServiceId);
                    sql.Parameters.AddWithValue("@service_amount", dado.ServiceAmount);
                    sql.Parameters.AddWithValue("@mileage_allowance", dado.MileageAllowance);
                    sql.Parameters.AddWithValue("@siopi", dado.Siopi);
                    sql.Parameters.AddWithValue("@customer_name", dado.CustomerName);
                    sql.Parameters.AddWithValue("@city", dado.City);
                    sql.Parameters.AddWithValue("@contact_name", dado.ContactName);
                    sql.Parameters.AddWithValue("@contact_phone", dado.ContactPhone);
                    sql.Parameters.AddWithValue("@coordinates", dado.Coordinates);
                    sql.Parameters.AddWithValue("@status", dado.Status);
                    sql.Parameters.AddWithValue("@pending_date", Util.DateOrNull(dado.PendingDate));
                    sql.Parameters.AddWithValue("@survey_date", Util.DateOrNull(dado.SurveyDate));
                    sql.Parameters.AddWithValue("@done_date", Util.DateOrNull(dado.DoneDate));
                    sql.Parameters.AddWithValue("@comments", dado.Comments);

                    sql.ExecuteNonQuery();

                    return sql.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(long id, ServiceOrder dado)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    UPDATE 
                        service_order 
                    SET 
                        title = @title, 
                        order_date = @order_date, 
                        deadline = @deadline, 
                        professional_id = @professional_id, 
                        service_id = @service_id, 
                        service_amount = service_amount, 
                        mileage_allowance = mileage_allowance, 
                        siopi = @siopi, 
                        customer_name = @customer_name, 
                        city = @city, 
                        contact_name = @contact_name, 
                        contact_phone = @contact_phone, 
                        coordinates = @coordinates, 
                        status = @status, 
                        pending_date = @pending_date, 
                        survey_date = @survey_date, 
                        done_date = @done_date, 
                        comments = @comments 
                    WHERE 
                        id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@title", dado.Title);
                    sql.Parameters.AddWithValue("@order_date", Util.DateOrNull(dado.OrderDate));
                    sql.Parameters.AddWithValue("@deadline", dado.Deadline);
                    sql.Parameters.AddWithValue("@professional_id", dado.ProfessionalId);
                    sql.Parameters.AddWithValue("@service_id", dado.ServiceId);
                    sql.Parameters.AddWithValue("@service_amount", dado.ServiceAmount);
                    sql.Parameters.AddWithValue("@mileage_allowance", dado.MileageAllowance);
                    sql.Parameters.AddWithValue("@siopi", dado.Siopi);
                    sql.Parameters.AddWithValue("@customer_name", dado.CustomerName);
                    sql.Parameters.AddWithValue("@city", dado.City);
                    sql.Parameters.AddWithValue("@contact_name", dado.ContactName);
                    sql.Parameters.AddWithValue("@contact_phone", dado.ContactPhone);
                    sql.Parameters.AddWithValue("@coordinates", dado.Coordinates);
                    sql.Parameters.AddWithValue("@status", dado.Status);
                    sql.Parameters.AddWithValue("@pending_date", Util.DateOrNull(dado.PendingDate));
                    sql.Parameters.AddWithValue("@survey_date", Util.DateOrNull(dado.SurveyDate));
                    sql.Parameters.AddWithValue("@done_date", Util.DateOrNull(dado.DoneDate));
                    sql.Parameters.AddWithValue("@comments", dado.Comments);
                    sql.Parameters.AddWithValue("@id", id);

                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void UpdateInvoiceId(long id, long invoice_id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var sql = new MySqlCommand(@"
                    UPDATE 
                        service_order 
                    SET 
                        invoice_id = @invoice_id 
                    WHERE 
                        id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@id", id);
                    sql.Parameters.AddWithValue("@invoice_id", invoice_id);

                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void UpdateStatus(long id, string status)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string changeDate = "";

                    switch (status)
                    {
                        case "RECEBIDA":
                            break;

                        case "PENDENTE":
                            changeDate = ", pending_date = @date";
                            break;

                        case "VISTORIADA":
                            changeDate = ", survey_date = @date";
                            break;

                        case "CONCLUÍDA":
                            changeDate = ", done_date = @date";
                            break;
                    }

                    var sql = new MySqlCommand(@$"
                    UPDATE 
                        service_order 
                    SET 
                        status = @status 
                        {changeDate} 
                    WHERE 
                        id = @id",
                        connection);

                    sql.Parameters.AddWithValue("@status", status);
                    sql.Parameters.AddWithValue("@date", DateTime.Now);
                    sql.Parameters.AddWithValue("@id", id);

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
                            service_order 
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
                        service_order 
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