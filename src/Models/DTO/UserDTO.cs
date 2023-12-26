using Dapper.Contrib.Extensions;

namespace FluxusApi.Models.DTO;

[Table("User")]
public class UserDTO
{
    public long Id { get; set; }
    public int ProfessionalId { get; set; }
    public bool TechnicianResponsible { get; set; }
    public bool LegalResponsible { get; set; }
    public bool UserActive { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
}