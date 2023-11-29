using Domain.Entities;

namespace Api.Models
{
    public sealed record CountryModel(int Id, string Name)
    {
        public static CountryModel FromDomain(Country country) => new CountryModel(country.Id, country.Name);
    }
}
