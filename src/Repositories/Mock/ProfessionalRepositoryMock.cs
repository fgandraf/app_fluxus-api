using System.Collections;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class ProfessionalRepositoryMock : RepositoryMock<ProfessionalDTO>, IProfessionalRepository
{

    public ProfessionalRepositoryMock()
        => InitializeData();
    
    public Task<IEnumerable> GetIndexAsync()
    {
        var index = Repository
            .OrderBy(x => x.Tag)
            .Select(x => new
            {
                Id = x.Id,
                Tag = x.Tag,
                Name = x.Name,
                Profession = x.Profession,
                Phone1 = x.Phone1
            })
            .ToList();

        return Task.FromResult<IEnumerable>(index);
    }

    public Task<IEnumerable> GetTagNameidAsync()
    {
        var professionals = Repository
            .OrderBy(x => x.Tag)
            .Select(x => new
            {
                Id = x.Id,
                Tag = x.Tag,
                Nameid = x.Profession == null ? "" : x.Profession.Substring(0,3) + ". " + (x.Name.Split(' ').Length > 1 ? x.Name.Split(' ')[0] + " " + x.Name.Split(' ')[x.Name.Split(' ').Length - 1] : "")
            });

        return Task.FromResult<IEnumerable>(professionals);
    }
    
    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<ProfessionalDTO>
            {
                new ProfessionalDTO { Id = 1, Tag = "A01", Name = "FELIPE FERREIRA GANDRA", Cpf = "333.666.888-99", Birthday = "03/01/1984 00:00:00", Profession = "ARQUITETO E URBANISTA", PermitNumber = "A55555-1", Association = "C.A.U. - CONSELHO DE ARQUITETURA E URBANISMO", Phone1 = "(14) 99829-0103", Phone2 = "(14) 4000-0000", Email = "fgandraf@gmail.com" },
                new ProfessionalDTO { Id = 2, Tag = "E01", Name = "JOSÉ DA SILVA SANTOS", Cpf = "123.456.789-00", Birthday = "01/03/1974 00:00:00", Profession = "ENGENHEIRO CIVIL", PermitNumber = "CREA 123456", Association = "C.R.E.A - CONSELHO REGIONAL DE ENGENHARIA E AGRONOMIA", Phone1 = "(11) 99999-8888", Phone2 = "(11) 3000-0000", Email = "jose@engenheiro.com" }
            };
        }
    }
    
}