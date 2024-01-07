using FluxusApi.Models.DTO;
using FluxusApi.Models.ViewModels;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class ProfileRepositoryMock : RepositoryMock<ProfileDTO>, IProfileRepository
{
    
    public ProfileRepositoryMock()
        => InitializeData();

    public Task<ProfileToPrintViewModel> GetToPrintAsync()
    {
        var profile = Repository
            .Where(x => x.Id == 1)
            .Select(x => new ProfileToPrintViewModel()
            {
                Cnpj = x.Cnpj,
                CompanyName = x.CompanyName,
                ContractNotice = x.ContractNotice,
                ContractNumber = x.ContractNumber
            })
            .FirstOrDefault();

        return Task.FromResult(profile);
    }

    public Task<string> GetTradingNameAsync()
    {
        return Task.FromResult(Repository
            .Where(x => x.Id == 1)
            .Select(x => x.TradingName)
            .SingleOrDefault());
    }

    
    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<ProfileDTO>
            {
                new ProfileDTO { Id = 1, Cnpj = "11.222.333/0001-00", TradingName = "ALÁFIA ARQUITETURA", CompanyName = "FELIPE FERREIRA GANDRA ARQUITETURA", StateId = "ISENTO", CityId = "123456", Address = "RUA DA EMPRESA, 00-11", Complement = "SALA X", District = "CENTRO", City = "BAURU", Zip = "17000-000", State = "SP", EstablishmentDate = DateTime.Parse("2019-10-29T00:00:00"), Phone1 = "(14) 3333-4444", Phone2 = "(14) 99829-0103", Email = "alafia@alafia.com", BankAccountName = "CAIXA ECONÔMICA FEDERAL", BankAccountType = "Poupança Integrada", BankAccountBranch = "0000", BankAccountDigit = "001", BankAccountNumber = "1234-5", ContractorName = "Caixa Econômica Federal", ContractNotice = "1234/2023", ContractNumber = "0000.11.2222.333/2023", ContractEstablished = DateTime.Parse("2023-03-17T00:00:00"), ContractStart = DateTime.Parse("2023-03-17T00:00:00"), ContractEnd = DateTime.Parse("2027-04-17T00:00:00") }
            };
            
        }
    }
    
}