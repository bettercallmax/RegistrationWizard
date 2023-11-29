using Domain.Entities;

namespace Api.Models
{
    public sealed record ProvinceModel(int Id, string Name)
    {
        public static ProvinceModel FromDomain(Province province) => new ProvinceModel(province.Id, province.Name);
    }
}
