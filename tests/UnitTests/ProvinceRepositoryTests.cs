using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data.Repositories;
using UnitTests.Utils;

namespace UnitTests;

public class ProvinceRepositoryTests
{
    [Fact]
    public async Task GetByCountryId_ShouldReturnAllEntitiesByThatId()
    {
        // Arrange
        const int countryId = 555;
        var dbContext = InMemoryDbContext.Create();
        var repository = new ProvinceRepository(dbContext);
        var country = new Country
        {
            Id = countryId,
            Name = "TestCountry"
        };
        var provinces = new List<Province>
        {
            new()
            {
                Id = 1,
                Name = "TestProvince1",
                Country = country
            },
            new()
            {
                Id = 2,
                Name = "TestProvince2",
                Country = country
            },
        };

        await dbContext.Provinces.AddRangeAsync(provinces);
        await dbContext.SaveChangesAsync();
        
        // Act
        var allProvinces = await repository.GetByCountryIdAsync(countryId: countryId, CancellationToken.None);

        // Assert
        allProvinces.Should().BeEquivalentTo(provinces, options => options.Excluding(x => x.Country));
    }
    
    [Fact]
    public async Task GetAsync_ShouldReturnEntry()
    {
        // Arrange
        var dbContext = InMemoryDbContext.Create();
        var repository = new ProvinceRepository(dbContext);
        var country = new Country
        {
            Id = 1,
            Name = "TestCountry"
        };
        var province = new Province
        {
            Id = 1,
            Name = "TestProvince",
            Country = country
        };

        await dbContext.AddAsync(country);
        await dbContext.Provinces.AddAsync(province);
        await dbContext.SaveChangesAsync();
        
        // Act
        var entry = await repository.GetAsync(id: 1, CancellationToken.None);

        // Assert
        entry.Should().NotBeNull();
        entry.Should().BeEquivalentTo(province, options => options.Excluding(x => x.Country));
    }
}