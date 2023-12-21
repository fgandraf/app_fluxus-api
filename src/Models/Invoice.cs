using Dapper.Contrib.Extensions;

namespace FluxusApi.Models;

[Table("Invoice")]
public class Invoice
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal SubtotalService { get; set; }
    public decimal SubtotalMileageAllowance { get; set; }
    public decimal Total { get; set; }
}