using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using UnitTests.Utils;

namespace UnitTests;

public class IdentityServiceTests
{
    [Fact]
    public async Task CheckPasswordAsync_ValidCredentials_ReturnsTrue()
    {
        // Arrange
        var mockUserManager = MockUserManager();
        var identityService = new IdentityService(mockUserManager.Object, InMemoryDbContext.Create());

        // Act
        var result = await identityService.CheckPasswordAsync("validLogin", "validPassword", CancellationToken.None);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckPasswordAsync_InvalidLogin_ReturnsFalse()
    {
        // Arrange
        var mockUserManager = MockUserManager(userExists: false);
        var identityService = new IdentityService(mockUserManager.Object, InMemoryDbContext.Create());

        // Act
        var action = () => identityService.CheckPasswordAsync("nonExistingLogin", "anyPassword", CancellationToken.None);

        // Assert
        await action.Should().ThrowExactlyAsync<NotFoundException>();
    }
    
    private static Mock<UserManager<ApplicationIdentityUser>> MockUserManager(bool userExists = true)
    {
        var users = new List<ApplicationIdentityUser>
        {
            new()
            {
                UserName = "validLogin",
                Email = "validLogin",
                NormalizedEmail = "validLogin".ToUpperInvariant(),
                Id = "1",
                DomainUser = new User
                {
                    Login = "TestLogin",
                    Province = new Province
                    {
                        Name = "TestProvince",
                        Country = new Country
                        {
                            Name = "TestCountry"
                        },
                    }
                }
            }
        }.AsQueryable().BuildMock();

        var userManagerMock = new Mock<UserManager<ApplicationIdentityUser>>(Mock.Of<IUserStore<ApplicationIdentityUser>>(), null, null, null, null, null, null, null, null);

        userManagerMock.Setup(m => m.Users).Returns(users);
        userManagerMock.Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationIdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync((ApplicationIdentityUser _, string _) => userExists);

        return userManagerMock;
    }
}