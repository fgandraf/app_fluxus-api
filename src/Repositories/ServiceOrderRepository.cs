using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class ServiceOrderRepository : Repository<ServiceOrder>
    {
        public ServiceOrderRepository(MySqlConnection connection) : base(connection) { }

        public IEnumerable GetOrdersFlow()
        {
            string query = @"
                SELECT 
                    A.Id,
                    CONCAT(
                        B.Tag, '-', A.City, '-', 
                        CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(A.ReferenceCode, '/', 1), '.', -1) AS UNSIGNED),
                        '\n\n',
                        REPLACE(A.CustomerName, ' ', ' '),
                        '\n- Prazo: ', DATE_FORMAT(A.Deadline, '%d/%m/%Y')
                    ) AS Title,
                    A.Status, 
                    A.ProfessionalId 
                FROM 
                    ServiceOrder as A 
                INNER JOIN
                    Service as B
                ON
                    B.Id = A.ServiceId
                WHERE 
                    InvoiceId = 0 
                ORDER BY 
                    OrderDate";

            return Connection.Query(query);
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

            return Connection.Query(query, new { invoiceId });
        }


        public IEnumerable GetDoneToInvoice()
        {
            string query = @"
                SELECT 
                    os.Id, 
                    os.OrderDate, 
                    os.ReferenceCode, 
                    pr.Tag Professional,
                    sr.Tag Service,
                    os.City, 
                    os.CustomerName, 
                    os.SurveyDate,
                    os.DoneDate, 
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
                    Invoiced = 0
                AND 
                    Status = 4
                ORDER BY 
                    DoneDate";

            return Connection.Query(query);
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

            return Connection.Query(query, param);
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

            return Connection.Query(query, new { invoiceId });
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

            return Connection.Query(query);
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

            return Connection.Execute(updateSQL, invoice);
        }


        public int UpdateStatus(int id, EnumStatus status)
        {
            string changeDate = "";
            switch (status)
            {
                case EnumStatus.RECEBIDA: break;
                case EnumStatus.PENDENTE: changeDate = ", PendingDate = @ActualDate "; break;
                case EnumStatus.VISTORIADA: changeDate = ", SurveyDate = @ActualDate "; break;
                case EnumStatus.CONCLUIDA: changeDate = ", DoneDate = @ActualDate "; break;
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

            return Connection.Execute(updateSQL, orderObj);
        }
    }
}