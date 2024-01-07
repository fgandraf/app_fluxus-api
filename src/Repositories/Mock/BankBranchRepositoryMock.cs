using System.Collections;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class BankBranchRepositoryMock : RepositoryMock<BankBranchDTO>, IBankBranchRepository
{
    
    public BankBranchRepositoryMock()
        => InitializeData();
    
    public Task<IEnumerable> GetIndexAsync()
    {
        var index = Repository
            .OrderBy(branch => branch.Id)
            .Select(branch => new
            {
                Id = branch.Id,
                Name = branch.Name,
                City = branch.City,
                Phone1 = branch.Phone1,
                Email = branch.Email
            })
            .ToList();
        return Task.FromResult<IEnumerable>(index);
    }

    public Task<IEnumerable> GetContactsAsync(string id)
    {
        var contacts = Repository
            .Where(branch => branch.Id == id)
            .Select(branch => new
            {
                Id = branch.Id,
                Name = branch.Name,
                Phone1 = branch.Phone1,
                Email = branch.Email
            })
            .ToList();
        return Task.FromResult<IEnumerable>(contacts);
    }
    
    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<BankBranchDTO>
            {
                new BankBranchDTO { Id = "0287", Name = "BARIRI", Address = "RUA SETE DE SETEMBRO, 1006", District = "CENTRO", City = "BARIRI", State = "SP", Phone1 = "(14) 3662-9140", Phone2 = "(14) 99829-0103", Email = "ag0287@caixa.gov.br" },
                new BankBranchDTO { Id = "0290", Name = "BAURU", Address = "RUA GUSTAVO MACIEL, 7-33", District = "CENTRO", City = "BAURU", State = "SP", Phone1 = "(14) 2106-9700", Phone2 = "", Email = "ag0290@caixa.gov.br" },
                new BankBranchDTO { Id = "0315", Name = "JAÚ", Address = "RUA TENENTE LOPES, 215", District = "CENTRO", City = "JAÚ", State = "SP", Phone1 = "(14) 3411-0200", Phone2 = "", Email = "ag0315sp04@caixa.gov.br" },
                new BankBranchDTO { Id = "0318", Name = "LINS", Address = "RUA 21 DE ABRIL, 187", District = "CENTRO", City = "LINS", State = "SP", Phone1 = "(14) 3511-1100", Phone2 = "", Email = "ag0318sp04@caixa.gov.br" },
                new BankBranchDTO { Id = "0962", Name = "LENÇÓIS PAULISTA", Address = "RUA SÃO PAULO, 245", District = "VILA MAMEDINA", City = "LENÇÓIS PAULISTA", State = "SP", Phone1 = "(14) 3269-1250", Phone2 = "", Email = "ag0962sp03@caixa.gov.br" },
                new BankBranchDTO { Id = "1920", Name = "QUATRO DE ABRIL", Address = "AVENIDA RIO BRANCO, 651", District = "CENTRO", City = "MARÍLIA", State = "SP", Phone1 = "(14) 3414-9800", Phone2 = "", Email = "ag1920@caixa.gov.br" },
                new BankBranchDTO { Id = "1996", Name = "CENTENÁRIO", Address = "RUA PRESENTE KENNEDY, 1-35", District = "CENTRO", City = "BAURU", State = "SP", Phone1 = "(14) 3233-3050", Phone2 = "", Email = "ag1996sp05@caixa.gov.br" },
                new BankBranchDTO { Id = "2032", Name = "TERRA ROXA", Address = "AVENIDA IZALTINO DO AMARAL CARVALHO, 2149", District = "CH BELA VISTA", City = "JAÚ", State = "SP", Phone1 = "(14) 3235-5750", Phone2 = "", Email = "ag2032sp@caixa.gov.br" },
                new BankBranchDTO { Id = "2141", Name = "ALTOS DA CIDADE", Address = "RUA GUSTAVO MACIEL, 24-14", District = "ALTOS DA CIDADE", City = "BAURU", State = "SP", Phone1 = "(14) 2107-3250", Phone2 = "", Email = "ag2141sp01@caixa.gov.br" },
                new BankBranchDTO { Id = "3254", Name = "JOÃO RIBEIRO DE BARROS", Address = "RUA MARECHAL DEODORO, 839", District = "VILA NOVA", City = "JAÚ", State = "SP", Phone1 = "(14) 2104-4200", Phone2 = "", Email = "a3254sp@caixa.gov.br" }
            };
        }
    }
    
}