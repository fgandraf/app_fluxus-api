using Dapper.Contrib.Extensions;

namespace FluxusApi.Models.DTO;

[Table("Professional")]
public class ProfessionalDTO
{
    public long Id { get; set; }
    public string Tag { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Birthday { get; set; }
    public string Profession { get; set; }
    public string PermitNumber { get; set; }
    public string Association { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Email { get; set; }
}