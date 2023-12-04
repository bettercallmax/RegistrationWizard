using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public IdentityService(
            UserManager<ApplicationIdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<bool> CheckPasswordAsync(string login, string password, CancellationToken token)
        {
            var identity = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant(), cancellationToken: token);
            if (identity == null)
            {
                throw new NotFoundException($"User with login {login} not found");
            }

            return await _userManager.CheckPasswordAsync(identity, password);
        }


        public async Task CreateUserAsync(string login, string password, Province province, CancellationToken token)
        {
            // Attach it to the context so that it does not attempt to add a new one
            _dbContext.Entry(province).State = EntityState.Unchanged;
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
                throw new DomainException($"Can't create user. {string.Join(',', values: identityResult?.Errors.Select(x => x.Code) ?? Array.Empty<string>())}");
            }
        }

        public async Task DeleteUserAsync(string login, CancellationToken token)
        {
            var identity = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant(), cancellationToken: token);
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
                .SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant(), cancellationToken: token);

            return identity?.DomainUser;
        }

        public async Task<User?> GetUserByLogin(string login, CancellationToken token)
        {
            var identity = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedEmail == login.ToUpperInvariant(), cancellationToken: token);

            return identity?.DomainUser;
        }
    }
}
