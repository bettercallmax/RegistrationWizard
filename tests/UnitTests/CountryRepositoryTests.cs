using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data.Repositories;
using UnitTests.Utils;

namespace UnitTests;

public class CountryRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities()
    {
        // Arrange
        var dbContext = InMemoryDbContext.Create();
        var repository = new CountryRepository(dbContext);
        var countries = new List<Country>
        {
            new()
            {
                Id = 1,
                Name = "TestCountry1"
            },
            new()
            {
                Id = 2,
                Name = "TestCountry2"
            },
        };

        await dbContext.Countries.AddRangeAsync(countries);
        await dbContext.SaveChangesAsync();
        
        // Act
        var allCountries = await repository.GetAllAsync(CancellationToken.None);

        // Assert
        allCountries.Should().BeEquivalentTo(countries);
    }
    
    [Fact]
    public async Task GetAsync_ShouldReturnEntry()
    {
        // Arrange
        var dbContext = InMemoryDbContext.Create();
        var repository = new CountryRepository(dbContext);
        var country = new Country
        {
            Id = 1,
            Name = "TestCountry"
        };

        await dbContext.Countries.AddAsync(country);
        await dbContext.SaveChangesAsync();
        
        // Act
        var entry = await repository.GetAsync(id: 1, CancellationToken.None);

        // Assert
        entry.Should().NotBeNull();
        entry.Should().BeEquivalentTo(country);
    }
}