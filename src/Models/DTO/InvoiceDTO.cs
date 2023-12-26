using Dapper.Contrib.Extensions;

namespace FluxusApi.Models.DTO;

[Table("Invoice")]
public class InvoiceDTO
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal SubtotalService { get; set; }
    public decimal SubtotalMileageAllowance { get; set; }
    public decimal Total { get; set; }
}