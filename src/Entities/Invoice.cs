using System;

namespace FluxusApi.Entities
{
    public class Invoice
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime IssueDate { get; set; }
        public double SubtotalService { get; set; }
        public double SubtotalMileageAllowance { get; set; }
        public double Total { get; set; }
    }
}
