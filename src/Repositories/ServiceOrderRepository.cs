using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ServiceOrderRepository : Repository<ServiceOrder>
    {
        public ServiceOrderRepository(MySqlConnection connection) : base(connection) { }

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

            return _connection.Query(query);
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

            return _connection.Query(query, new { invoiceId });
        }


        public IEnumerable GetDoneToInvoice()
        {
            string query = @"
                SELECT 
                    Id, OrderDate, ReferenceCode, ProfessionalId, ServiceId, City, 
                    CustomerName, SurveyDate, DoneDate, ServiceAmount, MileageAllowance
                FROM 
                    ServiceOrder 
                WHERE 
                    Invoiced = @invoiced 
                AND 
                    Status = @status
                ORDER BY 
                    DoneDate";

            return _connection.Query(query, new { invoiced = false, status = EnumStatus.DONE });
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
                    os.Invoiced
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
                    pr.Tag LIKE @professional AND sr.Tag LIKE @service AND os.City LIKE @city AND os.Status LIKE @status AND os.Invoiced LIKE @invoiced
                ORDER BY 
                    OrderDate";

            string[] filters = filter.Split(',');
            var param = new
            {
                professional = filters[0],
                service = filters[1],
                city = filters[2],
                status = filters[3],
                invoiced = filters[4]
            };

            return _connection.Query(query, param);
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

            return _connection.Query(query, new { invoiceId });
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

            return _connection.Query(query);
        }

        public int UpdateInvoiceId(int id, int invoiceId)
        {
            bool invoiced = false;
            if (invoiceId > 0)
                invoiced = true;

            string updateSQL = @"
                UPDATE
                    ServiceOrder
                SET
                    InvoiceId = @invoiceId,
                    Invoiced = @invoiced
                WHERE
                    Id = @Id";

            var invoice = new
            {
                InvoiceId = invoiceId,
                Invoiced = invoiced,
                Id = id
            };

            return _connection.Execute(updateSQL, invoice);
        }


        public int UpdateStatus(int id, EnumStatus status)
        {
            string changeDate = "";
            switch (status)
            {
                case EnumStatus.RECEIVED: break;
                case EnumStatus.PENDING: changeDate = ", PendingDate = @ActualDate "; break;
                case EnumStatus.SURVEYED: changeDate = ", SurveyDate = @ActualDate "; break;
                case EnumStatus.DONE: changeDate = ", DoneDate = @ActualDate "; break;
            }

            string updateSQL = @$"
                UPDATE
                    ServiceOrder
                SET
                    Status = @Status
                    {changeDate}
                WHERE
                    Id = @Id";

            var orderObj = new
            {
                Status = status,
                ActualDate = DateTime.Now,
                Id = id
            };

            return _connection.Execute(updateSQL, orderObj);
        }
    }
}