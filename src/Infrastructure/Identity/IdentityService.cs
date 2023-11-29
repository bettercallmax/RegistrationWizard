using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;

        public IdentityService(
            UserManager<ApplicationIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(string login, string password, CancellationToken token)
        {
            var identity = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant());
            if (identity == null)
            {
                throw new NotFoundException($"User with login {login} not found");
            }

            return await _userManager.CheckPasswordAsync(identity, password);
        }


        public async Task CreateUserAsync(string login, string password, Province province, CancellationToken token)
        {
            var identityResult = await _userManager.CreateAsync(new ApplicationIdentityUser
            {
                UserName = login,
                Email = login,
                DomainUser = new User
                {
                    Province = province,
                    Login = login,
                    
                }
            }, password);

            if (identityResult == null || identityResult.Errors.Any() || !identityResult.Succeeded)
            {
                throw new DomainException($"Can't create user. {string.Join(',', values: identityResult?.Errors.Select(x => x.Code))}");
            }
        }

        public async Task DeleteUserAsync(string login, CancellationToken token)
        {
            var identity = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant());
            if (identity == null)
            {
                throw new NotFoundException($"User with login {login} not found");
            }

            await _userManager.DeleteAsync(identity);
        }

        public async Task<User?> GetUserAsync(string login, CancellationToken token)
        {
            var identity = await _userManager
                .Users
                .Include(x => x.DomainUser.Province)
                .SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant());

            return identity?.DomainUser;
        }

        public async Task<User?> GetUserByLogin(string login, CancellationToken token)
        {
            var identity = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant());

            return identity?.DomainUser;
        }

        public Task<User?> GetUserByLogin(User user, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
