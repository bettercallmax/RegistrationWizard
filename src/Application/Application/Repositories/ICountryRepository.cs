using Domain.Entities;

namespace Application.Repositories
{
    public interface ICountryRepository
    {
        public Task<Country?> GetAsync(int id, CancellationToken token);

        public Task<List<Country>> GetAllAsync(CancellationToken token);
    }
}
