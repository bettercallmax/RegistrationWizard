using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Utils;

internal static class InMemoryDbContext
{
    public static ApplicationDbContext Create()
    {
        var dbName = $"InMemory_{Guid.NewGuid()}";

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        return new ApplicationDbContext(options);
    }
}