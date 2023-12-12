using Dapper.Contrib.Extensions;
using FluxusApi.Models.Enums;

namespace FluxusApi.ViewModels
{
    [Table("ServiceOrder")]
    public abstract class ServiceOrderViewModel
    {
        public long Id { get; set; }
        public string ReferenceCode { get; set; }
        public string Branch { get; set; }
        public string Title { get; set; }
        public string OrderDate { get; set; }
        public DateTime Deadline { get; set; }
        public string ProfessionalId { get; set; }
        public string ServiceId { get; set; }
        public decimal ServiceAmount { get; set; }
        public decimal MileageAllowance { get; set; }
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
        public bool Invoiced { get; set; }
        public long InvoiceId { get; set; } 
    }
}
