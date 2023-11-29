using Domain.Entities;

namespace Application.Repositories
{
    public interface IProvinceRepository
    {
        public Task<List<Province>> GetByCountryIdAsync(int countryId, CancellationToken token);

        public Task<Province?> GetAsync(int id, CancellationToken token);
    }
}
