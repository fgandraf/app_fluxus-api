using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class ServiceRepositoryMock : RepositoryMock<ServiceDTO>, IServiceRepository
{
    
    public ServiceRepositoryMock()
        => InitializeData();

    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<ServiceDTO>
            {
                new ServiceDTO { Id = 1, Tag = "E401", Description = "AC IMÓVEL URBANO (CONSTRUÇÃO, AMPLIAÇÃO OU REFORMA)", ServiceAmount = 369.00m, MileageAllowance = 37.00m },
                new ServiceDTO { Id = 2, Tag = "A401", Description = "AVA IMÓVEL COM VISTORIA LAUDO COMPL", ServiceAmount = 415.00m, MileageAllowance = 37.00m }
            };
        }
    }
    
}