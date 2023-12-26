using Dapper.Contrib.Extensions;

namespace FluxusApi.Models.DTO;

[Table("Service")]
public class ServiceDTO
{
    public long Id { get; set; }
    public string Tag { get; set; }
    public string Description { get; set; }
    public decimal ServiceAmount { get; set; }
    public decimal MileageAllowance { get; set; }
}