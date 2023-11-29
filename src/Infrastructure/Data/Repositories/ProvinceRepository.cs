using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    internal class ProvinceRepository : IProvinceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProvinceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Province?> GetAsync(int id, CancellationToken token) =>
            _dbContext.Provinces
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);

        public Task<List<Province>> GetByCountryIdAsync(int countryId, CancellationToken token) =>
            _dbContext.Provinces
                .AsNoTracking()
                .Where(x => x.Country.Id == countryId)
                .ToListAsync();
    }
}
