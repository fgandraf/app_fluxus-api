using Dapper.Contrib.Extensions;

namespace FluxusApi.Entities
{
    [Table("ServiceOrder")]
    public class ServiceOrder
    {
        public long Id { get; set; }
        public string ReferenceCode { get; set; }
        public string Branch { get; set; }
        public string Title { get; set; }
        public string OrderDate { get; set; }
        public DateTime Deadline { get; set; }
        public string ProfessionalId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceAmount { get; set; }
        public string MileageAllowance { get; set; }
        public bool Siopi { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Coordinates { get; set; }
        public EnumStatus Status { get; set; }
        public string PendingDate { get; set; }
        public string SurveyDate { get; set; }
        public string DoneDate { get; set; }
        public string Comments { get; set; }
        public bool Invoiced { get; set; }
        public long InvoiceId { get; set; } 
    }
}
