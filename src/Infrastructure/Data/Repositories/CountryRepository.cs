using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    internal class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CountryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Country>> GetAllAsync(CancellationToken token) => _dbContext.Countries.AsNoTracking().ToListAsync();

        public Task<Country?> GetAsync(int id, CancellationToken token) => _dbContext.Countries?.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }
}
