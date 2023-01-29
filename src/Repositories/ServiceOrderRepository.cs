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

                    var command = new MySqlCommand(@"
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

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var orders = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(reader["id"]),
                                ReferenceCode = Convert.ToString(reader["reference_code"]),
                                Title = Convert.ToString(reader["title"]),
                                Status = Convert.ToString(reader["status"]),
                                ProfessionalId = Convert.ToString(reader["professional_id"])
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


        public ArrayList GetInvoiced(int invoiceId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                    SELECT 
                        os.id, 
                        os.order_date, 
                        os.reference_code, 
                        os.professional_id,
                        pr.tag professional,
                        sr.tag service,
                        os.city, 
                        os.customer_name, 
                        os.survey_date, 
                        os.done_date, 
                        os.invoice_id, 
                        os.status, 
                        os.service_amount, 
                        os.mileage_allowance 
                    FROM 
                        service_order os
                    INNER JOIN
                        service sr
                    ON
                        os.service_id = sr.id
                    INNER JOIN
                        professional pr
                    ON
                        os.professional_id = pr.id
                    WHERE 
                        invoice_id = @invoice_id
                    ORDER BY 
                        done_date",
                        connection);

                    command.Parameters.AddWithValue("@invoice_id", invoiceId);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var orders = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(reader["id"]),
                                OrderDate = Convert.ToDateTime(reader["order_date"]),
                                ReferenceCode = Convert.ToString(reader["reference_code"]),
                                Professional = Convert.ToString(reader["professional"]),
                                ProfessionalId = Convert.ToString(reader["professional_id"]),
                                Service = Convert.ToString(reader["service"]),
                                City = Convert.ToString(reader["city"]),
                                CustomerName = Convert.ToString(reader["customer_name"]),
                                SurveyDate = Convert.ToDateTime(reader["survey_date"]),
                                DoneDate = Convert.ToDateTime(reader["done_date"]),
                                InvoiceId = Convert.ToInt64(reader["invoice_id"]),
                                Status = Convert.ToString(reader["status"]),
                                ServiceAmount = Convert.ToDouble(reader["service_amount"]),
                                MileageAllowance = Convert.ToDouble(reader["mileage_allowance"])
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

                    var command = new MySqlCommand(@"
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

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var orders = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(reader["id"]),
                                OrderDate = Convert.ToDateTime(reader["order_date"]),
                                ReferenceCode = Convert.ToString(reader["reference_code"]),
                                ProfessionalId = Convert.ToString(reader["professional_id"]),
                                ServiceId = Convert.ToString(reader["service_id"]),
                                City = Convert.ToString(reader["city"]),
                                CustomerName = Convert.ToString(reader["customer_name"]),
                                SurveyDate = Convert.ToDateTime(reader["survey_date"]),
                                DoneDate = Convert.ToDateTime(reader["done_date"]),
                                ServiceAmount = Convert.ToDouble(reader["service_amount"]),
                                MileageAllowance = Convert.ToDouble(reader["mileage_allowance"])
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

        public ArrayList GetFiltered(string filter)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@$"
                        SELECT 
                            os.id,
                            os.status,
                            pr.tag professional,
                            os.order_date,
                            os.reference_code,
                            sr.tag service,
                            os.city,
                            os.customer_name,
                            os.survey_date,
                            os.done_date,
                            os.invoice_id
                        FROM
                            service_order os
                        INNER JOIN
                            service sr
                        ON
                            os.service_id = sr.id
                        INNER JOIN
                            professional pr
                        ON
                            os.professional_id = pr.id
                        WHERE 
                            {filter}
                        ORDER BY 
                            order_date",
                        connection);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var orders = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic order = new
                            {
                                Id = Convert.ToInt64(reader["id"]),
                                Status = Convert.ToString(reader["status"]),
                                Professional = Convert.ToString(reader["professional"]),
                                OrderDate = Convert.ToString(reader["order_date"]),
                                ReferenceCode = Convert.ToString(reader["reference_code"]),
                                Service = Convert.ToString(reader["service"]),
                                City = Convert.ToString(reader["city"]),
                                CustomerName = Convert.ToString(reader["customer_name"]),
                                SurveyDate = Convert.ToString(reader["survey_date"]),
                                DoneDate = Convert.ToString(reader["done_date"]),
                                InvoiceId = Convert.ToInt64(reader["invoice_id"])
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


        public ArrayList GetProfessionals(int invoiceId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
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

                    command.Parameters.AddWithValue("@invoice_id", invoiceId);

                    var reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        var professionals = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic professional = new
                            {
                                ProfessionalId = Convert.ToString(reader["professional_id"]),
                                Nameid = Convert.ToString(reader["nameid"])
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

                    var command = new MySqlCommand(@"
                    SELECT DISTINCT 
                        city 
                    FROM 
                        service_order 
                    ORDER BY 
                        city",
                        connection);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var cities = new ArrayList();

                        while (reader.Read())
                        {
                            dynamic city = new
                            {
                                City = Convert.ToString(reader["city"])
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


        public ServiceOrder GetBy(int id)
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
                            service_order 
                        WHERE 
                            id = @id",
                            connection);

                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var order = new ServiceOrder();

                        if (reader.Read())
                        {
                            order.Id = Convert.ToInt64(reader["id"]);
                            order.ReferenceCode = Convert.ToString(reader["reference_code"]);
                            order.Branch = Convert.ToString(reader["branch"]);
                            order.Title = Convert.ToString(reader["title"]);
                            order.OrderDate = Convert.ToString(reader["order_date"]);
                            order.Deadline = Convert.ToDateTime(reader["deadline"]);
                            order.ProfessionalId = Convert.ToString(reader["professional_id"]);
                            order.ServiceId = Convert.ToString(reader["service_id"]);
                            order.ServiceAmount = Convert.ToString(reader["service_amount"]);
                            order.MileageAllowance = Convert.ToString(reader["mileage_allowance"]);
                            order.Siopi = Convert.ToBoolean(reader["siopi"]);
                            order.CustomerName = Convert.ToString(reader["customer_name"]);
                            order.City = Convert.ToString(reader["city"]);
                            order.ContactName = Convert.ToString(reader["contact_name"]);
                            order.ContactPhone = Convert.ToString(reader["contact_phone"]);
                            order.Coordinates = Convert.ToString(reader["coordinates"]);
                            order.Status = Convert.ToString(reader["status"]);
                            order.PendingDate = Convert.ToString(reader["pending_date"]);
                            order.SurveyDate = Convert.ToString(reader["survey_date"]);
                            order.DoneDate = Convert.ToString(reader["done_date"]);
                            order.Comments = Convert.ToString(reader["comments"]);
                            order.InvoiceId = Convert.ToInt64(reader["invoice_id"]);
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


        public long Insert(ServiceOrder serviceOrder)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                    INSERT INTO service_order
                        (title, reference_code, branch, order_date, deadline, professional_id, service_id, 
                        service_amount, mileage_allowance, siopi, customer_name, city, contact_name, 
                        contact_phone, coordinates, status, pending_date, survey_date, done_date, comments) 
                    VALUES 
                        (@title, @reference_code, @branch, @order_date, @deadline, @professional_id, @service_id, 
                        @service_amount, @mileage_allowance, @siopi, @customer_name, @city, @contact_name, 
                        @contact_phone, @coordinates, @status, @pending_date, @survey_date, @done_date, @comments)",
                        connection);

                    command.Parameters.AddWithValue("@title", serviceOrder.Title);
                    command.Parameters.AddWithValue("@reference_code", serviceOrder.ReferenceCode);
                    command.Parameters.AddWithValue("@branch", serviceOrder.Branch);
                    command.Parameters.AddWithValue("@order_date", serviceOrder.OrderDate);
                    command.Parameters.AddWithValue("@deadline", serviceOrder.Deadline);
                    command.Parameters.AddWithValue("@professional_id", serviceOrder.ProfessionalId);
                    command.Parameters.AddWithValue("@service_id", serviceOrder.ServiceId);
                    command.Parameters.AddWithValue("@service_amount", serviceOrder.ServiceAmount);
                    command.Parameters.AddWithValue("@mileage_allowance", serviceOrder.MileageAllowance);
                    command.Parameters.AddWithValue("@siopi", serviceOrder.Siopi);
                    command.Parameters.AddWithValue("@customer_name", serviceOrder.CustomerName);
                    command.Parameters.AddWithValue("@city", serviceOrder.City);
                    command.Parameters.AddWithValue("@contact_name", serviceOrder.ContactName);
                    command.Parameters.AddWithValue("@contact_phone", serviceOrder.ContactPhone);
                    command.Parameters.AddWithValue("@coordinates", serviceOrder.Coordinates);
                    command.Parameters.AddWithValue("@status", serviceOrder.Status);
                    command.Parameters.AddWithValue("@pending_date", serviceOrder.PendingDate);
                    command.Parameters.AddWithValue("@survey_date", serviceOrder.SurveyDate);
                    command.Parameters.AddWithValue("@done_date", serviceOrder.DoneDate);
                    command.Parameters.AddWithValue("@comments", serviceOrder.Comments);

                    command.ExecuteNonQuery();

                    return command.LastInsertedId;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(ServiceOrder serviceOrder)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
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

                    command.Parameters.AddWithValue("@title", serviceOrder.Title);
                    command.Parameters.AddWithValue("@order_date", serviceOrder.OrderDate);
                    command.Parameters.AddWithValue("@deadline", serviceOrder.Deadline);
                    command.Parameters.AddWithValue("@professional_id", serviceOrder.ProfessionalId);
                    command.Parameters.AddWithValue("@service_id", serviceOrder.ServiceId);
                    command.Parameters.AddWithValue("@service_amount", serviceOrder.ServiceAmount);
                    command.Parameters.AddWithValue("@mileage_allowance", serviceOrder.MileageAllowance);
                    command.Parameters.AddWithValue("@siopi", serviceOrder.Siopi);
                    command.Parameters.AddWithValue("@customer_name", serviceOrder.CustomerName);
                    command.Parameters.AddWithValue("@city", serviceOrder.City);
                    command.Parameters.AddWithValue("@contact_name", serviceOrder.ContactName);
                    command.Parameters.AddWithValue("@contact_phone", serviceOrder.ContactPhone);
                    command.Parameters.AddWithValue("@coordinates", serviceOrder.Coordinates);
                    command.Parameters.AddWithValue("@status", serviceOrder.Status);
                    command.Parameters.AddWithValue("@pending_date", serviceOrder.PendingDate);
                    command.Parameters.AddWithValue("@survey_date", serviceOrder.SurveyDate);
                    command.Parameters.AddWithValue("@done_date", serviceOrder.DoneDate);
                    command.Parameters.AddWithValue("@comments", serviceOrder.Comments);
                    command.Parameters.AddWithValue("@id", serviceOrder.Id);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void UpdateInvoiceId(int id, int invoiceId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                    UPDATE 
                        service_order 
                    SET 
                        invoice_id = @invoice_id 
                    WHERE 
                        id = @id",
                        connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@invoice_id", invoiceId);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void UpdateStatus(int id, string status)
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

                    var command = new MySqlCommand(@$"
                        UPDATE 
                            service_order 
                        SET 
                            status = @status 
                            {changeDate} 
                        WHERE 
                            id = @id",
                            connection);

                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@date", DateTime.Now);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
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
                                service_order 
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
                                service_order 
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