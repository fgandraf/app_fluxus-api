using Dapper.Contrib.Extensions;

namespace FluxusApi.Entities
{
    [Table("Service")]
    public class Service
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public decimal ServiceAmount { get; set; }
        public decimal MileageAllowance { get; set; }
    }
}
