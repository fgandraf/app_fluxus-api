using Dapper.Contrib.Extensions;

namespace FluxusApi.Entities
{
    [Table("Service")]
    public class Service
    {
        public long Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public string ServiceAmount { get; set; }
        public string MileageAllowance { get; set; }
    }
}
