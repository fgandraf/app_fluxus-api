using Dapper.Contrib.Extensions;

namespace FluxusApi.Models
{
    [Table("User")]
    public class User
    {
        public long Id { get; set; }
        public int ProfessionalId { get; set; }
        public bool TechnicianResponsible { get; set; }
        public bool LegalResponsible { get; set; }
        public bool UserActive { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}