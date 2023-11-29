using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class ServiceProviderExtensions
    {
        public static async Task ApplyMigrationsAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateAsyncScope();
            await ApplyMigrationForContext<ApplicationDbContext>(scope);
        }

        public static async Task SeedTestDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateAsyncScope();
            var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            await appContext.Database.EnsureCreatedAsync();
            if (!await appContext.Countries.AnyAsync())
            {
                var countries = Enumerable
                 .Range(1, 3)
                 .Select(index => new Country
                 {
                     Name = $"Country {index}"
                 })
                 .ToList();

                await appContext.Countries.AddRangeAsync(countries);
                await appContext.SaveChangesAsync();


                var provinces = countries.Select(country =>
                {
                    return Enumerable
                        .Range(country.Id, 3)
                        .Select(index => new Province
                        {
                            Name = $"Province {country.Id}.{index}",
                            Country = country,
                        });
                })
                .SelectMany(provinces => provinces)
                .ToList();

                await appContext.Provinces.AddRangeAsync(provinces);
                await appContext.SaveChangesAsync();
            }

            var user = await identityService.GetUserAsync("test@test.com", CancellationToken.None);
            if (user == null)
            {
                var province = appContext.Provinces.First();
                await identityService.CreateUserAsync("test@test.com", "testPassword", province, CancellationToken.None);
            }
        }

        private static Task ApplyMigrationForContext<T>(IServiceScope serviceScope)
            where T : DbContext
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService(typeof(T)) as T;
            return dbContext!.Database.MigrateAsync();
        }
    }
}
