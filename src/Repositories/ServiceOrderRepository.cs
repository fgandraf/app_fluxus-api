using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Components.Routing;
using Dapper;

namespace FluxusApi.Repositories
{


    public class ServiceOrderRepository
    {
        private string _connectionString = string.Empty;

        public ServiceOrderRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public IEnumerable GetOrdersFlow()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var serviceOrders = connection.Query(@"
                        SELECT 
                            Id, 
                            ReferenceCode, 
                            Title, 
                            Status, 
                            ProfessionalId 
                        FROM 
                            ServiceOrder 
                        WHERE 
                            InvoiceId = 0 
                        ORDER BY 
                            OrderDate");
                    return serviceOrders;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetInvoiced(int invoiceId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var serviceOrders = connection.Query(@"
                        SELECT 
                            os.Id, 
                            os.OrderDate, 
                            os.ReferenceCode, 
                            os.ProfessionalId,
                            pr.Tag Professional,
                            sr.Tag Service,
                            os.City, 
                            os.CustomerName, 
                            os.SurveyDate, 
                            os.DoneDate, 
                            os.InvoiceId, 
                            os.Status, 
                            os.ServiceAmount, 
                            os.MileageAllowance 
                        FROM 
                            ServiceOrder os
                        INNER JOIN
                            Service sr
                        ON
                            os.ServiceId = sr.Id
                        INNER JOIN
                            Professional pr
                        ON
                            os.ProfessionalId = pr.Id
                        WHERE 
                            InvoiceId = @invoiceId
                        ORDER BY 
                            DoneDate", new { invoiceId });
                    return serviceOrders;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetDoneToInvoice()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var serviceOrders = connection.Query(@"
                        SELECT 
                            Id, OrderSate, ReferenceCode, ProfessionalId, ServiceId, City, 
                            CustomerName, SurveyDate, DoneDate, ServiceAmount, MileageAllowance 
                        FROM 
                            ServiceOrder 
                        WHERE 
                            InvoiceId = 0 
                        AND 
                            Status = 'CONCLUÍDA' 
                        ORDER BY 
                            DoneDate");
                    return serviceOrders;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public IEnumerable GetFiltered(string filter)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var serviceOrders = connection.Query(@"
                        SELECT 
                            os.Id,
                            os.Status,
                            pr.Tag Professional,
                            os.OrderDate,
                            os.ReferenceCode,
                            sr.Tag Service,
                            os.City,
                            os.CustomerName,
                            os.SurveyDate,
                            os.DoneDate,
                            os.InvoiceId
                        FROM
                            ServiceOrder os
                        INNER JOIN
                            Service sr
                        ON
                            os.ServiceId = sr.Id
                        INNER JOIN
                            Professional pr
                        ON
                            os.ProfessionalId = pr.Id
                        WHERE 
                            @filter
                        ORDER BY 
                            OrderDate", new { filter });
                    return serviceOrders;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetProfessional(int invoiceId)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var professional = connection.QueryFirst(@"
                        SELECT DISTINCT 
                            t1.ProfessionalId, 
                            t2.Nameid 
                        FROM 
                            ServiceOrder t1 
                        INNER JOIN 
                            Professional t2 
                        on 
                            t1.ProfessionalId = t2.Id 
                        WHERE 
                            t1.InvoiceId = @invoiceId 
                        ORDER BY 
                            t2.Nameid", new { invoiceId });
                    return professional;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetOrderedCities()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var cities = connection.Query(@"
                        SELECT DISTINCT 
                            City 
                        FROM 
                            ServiceOrder 
                        ORDER BY 
                            City");
                    return cities;
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
                    var serviceOrder = connection.QueryFirst(@"
                        SELECT 
                            * 
                        FROM 
                            ServiceOrder 
                        WHERE 
                            Id = @id", new { id });
                    return serviceOrder;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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