using Application.Repositories;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Data.Repositories;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceStorage(
            this IServiceCollection services,
            string? applicationDbConnectionString) =>
            services
                .AddDbContext<ApplicationDbContext>(options => options.AddInterceptors(new BaseEntityInterceptor(TimeProvider.System)).UseSqlite(applicationDbConnectionString))
                .AddIdentityCore<ApplicationIdentityUser>(options =>
                {
                    options.Password.RequireLowercase = true;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .Services;

        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<IIdentityService, IdentityService>();

        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ICountryRepository, CountryRepository>()
                .AddScoped<IProvinceRepository, ProvinceRepository>();

    }
}
