using Domain.Entities;

namespace Application.Services
{
    public interface IIdentityService
    {
        Task<User?> GetUserAsync(string login, CancellationToken token);

        Task CreateUserAsync(string login, string password, Province province, CancellationToken token);

        Task DeleteUserAsync(string login, CancellationToken token);

        Task<bool> CheckPasswordAsync(string login, string password, CancellationToken token);
    }
}
