using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;

namespace FluxusApi.Repositories.Mock;

public class UserRepositoryMock : RepositoryMock<UserDTO>, IUserRepository
{
    
    public UserRepositoryMock()
        => InitializeData();

    public Task<UserDTO> GetByUserNameAsync(string userName)
        => Task.FromResult(Repository.SingleOrDefault(x => x.UserName == userName));
    
    public Task<UserDTO> GetByProfessionalIdAsync(int professionalId)
        => Task.FromResult<UserDTO>(Repository.SingleOrDefault(x => x.ProfessionalId == professionalId));
    

    private void InitializeData()
    {
        if (Repository.Count == 0)
        {
            Repository = new List<UserDTO>
            {
                new UserDTO { Id = 1, ProfessionalId = 1, TechnicianResponsible = true, LegalResponsible = true, UserActive = true, UserName = "admin", UserPassword = "10000.eQfaylUHmaNv2jK6rUTqQA==./7QU0+9c/pbdGm2Lvw6WSQjde5C9l7Q4P0upr5DTTsw=" }
            };
            
        }
    }
}