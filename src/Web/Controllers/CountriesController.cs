using Api.Controllers.Abstract;
using Api.Models;
using Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("country")]
    public class CountriesController : ApiController
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IProvinceRepository _provinceRepository;

        public CountriesController(
            ICountryRepository countryRepository,
            IProvinceRepository provinceRepository,
            ILogger<CountriesController> logger) : base(logger)
        {
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<CountryModel>> GetAllAsync(CancellationToken token)
        {
            var countries = await _countryRepository.GetAllAsync(token);

            return countries.Select(CountryModel.FromDomain);
        }

        [HttpGet("{id}/provinces")]
        [AllowAnonymous]
        public async Task<IEnumerable<ProvinceModel>> GetProvincesByCountryIdAsync(int id, CancellationToken cancellationToken) 
        {
            var provinces = await _provinceRepository.GetByCountryIdAsync(id, cancellationToken);

            return provinces.Select(ProvinceModel.FromDomain);
        }
    }
}
