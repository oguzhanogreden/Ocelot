using Ocelot.Configuration.File;
using Xunit;

namespace Ocelot.UnitTests.Configuration.FileModels;

public class FileGlobalRateLimitRuleTests
{
    [Fact]
    public void should_initialize_with_default_values()
    {
        var rule = new FileGlobalRateLimitRule();

        rule.RouteKeys.ShouldNotBeNull();
        rule.RouteKeys.ShouldBeEmpty();
        rule.ClientWhitelist.ShouldNotBeNull();
        rule.ClientWhitelist.ShouldBeEmpty();
        rule.EnableRateLimiting.ShouldBeFalse();
        rule.Limit.ShouldBe(0);
        rule.PeriodTimespan.ShouldBe(0);
        rule.Period.ShouldBeNull();
    }

    [Fact]
    public void should_set_and_get_properties()
    {
        var rule = new FileGlobalRateLimitRule
        {
            RouteKeys = new List<string> { "users", "posts" },
            ClientWhitelist = new List<string> { "client1" },
            EnableRateLimiting = true,
            Period = "1s",
            PeriodTimespan = 1,
            Limit = 100
        };

        rule.RouteKeys.Count.ShouldBe(2);
        rule.RouteKeys.ShouldContain("users");
        rule.RouteKeys.ShouldContain("posts");
        rule.ClientWhitelist.Count.ShouldBe(1);
        rule.ClientWhitelist.ShouldContain("client1");
        rule.EnableRateLimiting.ShouldBeTrue();
        rule.Period.ShouldBe("1s");
        rule.PeriodTimespan.ShouldBe(1);
        rule.Limit.ShouldBe(100);
    }
}
