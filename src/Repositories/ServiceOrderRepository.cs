using System;
using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Components.Routing;
using Dapper;
using Mysqlx.Crud;

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


        public int Insert(ServiceOrder serviceOrder)
        {
            try
            {
                string insertSQL = @"
                    INSERT INTO ServiceOrder
                        (Title, ReferenceCode, Branch, OrderDate, Deadline, ProfessionalId, ServiceId, 
                        ServiceAmount, MileageAllowance, Siopi, CustomerName, City, ContactName, 
                        ContactPhone, Coordinates, Status, PendingDate, SurveyDate, DoneDate, Comments) 
                    VALUES 
                        (@Title, @ReferenceCode, @Branch, @OrderDate, @Deadline, @ProfessionalId, @ServiceId, 
                        @ServiceAmount, @MileageAllowance, @Siopi, @CustomerName, @City, @ContactName, 
                        @ContactPhone, @Coordinates, @Status, @PendingDate, @SurveyDate, @DoneDate, @Comments)";

                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(insertSQL, serviceOrder);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(ServiceOrder serviceOrder)
        {
            try
            {
                string updateSQL = @"
                    UPDATE 
                        ServiceOrder 
                    SET 
                        Title = @Title, 
                        OrderDate = @OrderDate, 
                        Deadline = @Deadline, 
                        ProfessionalId = @ProfessionalId, 
                        ServiceId = @ServiceId, 
                        ServiceAmount = ServiceAmount, 
                        MileageAllowance = MileageAllowance, 
                        Siopi = @Siopi, 
                        CustomerName = @CustomerName, 
                        City = @City, 
                        ContactName = @ContactName, 
                        ContactPhone = @ContactPhone, 
                        Coordinates = @Coordinates, 
                        Status = @Status, 
                        PendingDate = @PendingDate, 
                        SurveyDate = @SurveyDate, 
                        DoneDate = @DoneDate, 
                        Comments = @Comments 
                    WHERE 
                        Id = @Id";

                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, serviceOrder);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int UpdateInvoiceId(int id, int invoiceId)
        {
            try
            {
                string updateSQL = @"
                    UPDATE
                        ServiceOrder
                    SET
                        InvoiceId = @InvoiceId
                    WHERE
                        Id = @Id";

                var invoice = new
                {
                    InvoiceId = invoiceId,
                    Id = id
                };

                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, invoice);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int UpdateStatus(int id, string status)
        {
            try
            {
                string changeDate = string.Empty;
                switch (status)
                {
                    case "RECEBIDA": break;
                    case "PENDENTE": changeDate = ", PendingDate = @Date"; break;
                    case "VISTORIADA": changeDate = ", SurveyDate = @Date"; break;
                    case "CONCLUÍDA": changeDate = ", DoneDate = @Date"; break;
                }

                string updateSQL = @"
                    UPDATE 
                        ServiceOrder 
                    SET 
                        Status = @Status 
                        @changeDate 
                    WHERE 
                        Id = @Id";

                var order = new
                {
                    Status = status,
                    Date = DateTime.Now,
                    Id = id
                };

                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Execute(updateSQL, order);
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
                        ServiceOrder 
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