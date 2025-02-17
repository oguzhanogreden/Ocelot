using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Validator;

namespace Ocelot.UnitTests.Configuration.Validator;

public class FileGlobalRateLimitRuleValidatorTests
{
    private readonly FileConfigurationFluentValidator _validator;

    public FileGlobalRateLimitRuleValidatorTests()
    {
        var serviceProvider = new ServiceCollection().BuildServiceProvider();
        var authProvider = new Mock<IAuthenticationSchemeProvider>();
        var routeValidator = new RouteFluentValidator(authProvider.Object, new HostAndPortValidator(), new FileQoSOptionsFluentValidator(serviceProvider));
        var globalValidator = new FileGlobalConfigurationFluentValidator(new FileQoSOptionsFluentValidator(serviceProvider));
        _validator = new FileConfigurationFluentValidator(serviceProvider, routeValidator, globalValidator);
    }

    [Fact]
    public async Task Should_Pass_Validation_When_RouteKeys_Match_Existing_Routes()
    {
        // Arrange
        var config = new FileConfiguration
        {
            Routes = new List<FileRoute>
            {
                new()
                {
                    Key = "users",
                    UpstreamPathTemplate = "/a",
                    DownstreamPathTemplate = "/b",
                    DownstreamHostAndPorts = new List<FileHostAndPort>()
                    {
                        new("w", 80),
                    },
                },
                new()
                {
                    Key = "posts",
                    UpstreamPathTemplate = "/x",
                    DownstreamPathTemplate = "/y",
                    DownstreamHostAndPorts = new List<FileHostAndPort>()
                    {
                        new("w", 80),
                    },
                },
            },
            GlobalRateLimitRules = new List<FileGlobalRateLimitRule>
            {
                new()
                {
                    RouteKeys = new List<string> { "users", "posts" },
                    EnableRateLimiting = true,
                    Period = "1s",
                    PeriodTimespan = 1,
                    Limit = 100,
                },
            },
        };

        // Act
        var result = await _validator.IsValid(config);

        // Assert
        Assert.False(result.IsError);
        Assert.False(result.Data.IsError);
    }

    [Fact]
    public async Task Should_Fail_Validation_When_RouteKey_Does_Not_Exist()
    {
        // Arrange
        var config = new FileConfiguration
        {
            Routes = new List<FileRoute>
            {
                new() { Key = "users", UpstreamPathTemplate = "/a", DownstreamPathTemplate = "/b"}
            },
            GlobalRateLimitRules = new List<FileGlobalRateLimitRule>
            {
                new()
                {
                    RouteKeys = new List<string> { "users", "nonexistent" },
                    EnableRateLimiting = true,
                    Period = "1s",
                    PeriodTimespan = 1,
                    Limit = 100
                }
            }
        };

        // Act
        var result = await _validator.IsValid(config);

        // Assert
        // Assert.True(result.IsError);
        Assert.True(result.Data.IsError);
        Assert.Contains(result.Data.Errors, e => e.Message.Contains("RouteKeys"));
    }
}
