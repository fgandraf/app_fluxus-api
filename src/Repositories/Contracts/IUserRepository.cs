using System.Collections;
using FluxusApi.Models;
using FluxusApi.Models.DTO;

namespace FluxusApi.Repositories.Contracts;

public interface IUserRepository
{
    Task<long> InsertAsync(UserDTO model);
    Task<bool> UpdateAsync(UserDTO model);
    Task<bool> DeleteAsync(UserDTO model);
    Task<UserDTO> GetByUserNameAsync(string userName);
    Task<IEnumerable> GetByProfessionalIdAsync(int professionalId);
    Task<UserDTO> GetAsync(int id);
}