using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
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
            string query = @"
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
                    OrderDate";

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


        public IEnumerable GetInvoiced(int invoiceId)
        {
            string query = @"
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
                    DoneDate";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Query(query, new { invoiceId });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetDoneToInvoice()
        {
            string query = @"
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
                    DoneDate";

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

        public IEnumerable GetFiltered(string filter)
        {
            string query = @"
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
                    OrderDate";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Query(query, new { filter });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetProfessional(int invoiceId)
        {
            string query = @"
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
                    t2.Nameid";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.QueryFirst(query, new { invoiceId });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetOrderedCities()
        {
            string query = @"
                SELECT DISTINCT 
                    City 
                FROM 
                    ServiceOrder 
                ORDER BY 
                    City";

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
                    ServiceOrder 
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


        public int Insert(ServiceOrder serviceOrder)
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

            try
            {
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

            try
            {
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

            try
            {
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

            try
            {
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
            string deleteSQL = @"
                DELETE FROM 
                    ServiceOrder 
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