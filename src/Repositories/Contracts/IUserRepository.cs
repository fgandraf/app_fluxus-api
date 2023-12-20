using FluxusApi.Models;

namespace FluxusApi.Repositories.Contracts;

public interface IUserRepository
{
    Task<long> InsertAsync(User model);
    Task<bool> UpdateAsync(User model);
    Task<bool> DeleteAsync(User model);
    Task<User> GetByUserNameAsync(string userName);
    Task<User> GetAsync(int id);
}