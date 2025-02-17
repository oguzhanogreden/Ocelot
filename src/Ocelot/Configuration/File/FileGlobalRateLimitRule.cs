namespace Ocelot.Configuration.File;

public class FileGlobalRateLimitRule
{
    /// <summary>
    /// List of route identifiers to which this rule applies.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of route keys that this rate limit rule applies to.
    /// </value>
    public List<string> RouteKeys { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether rate limiting is enabled.
    /// </summary>
    /// <value>
    /// A boolean value indicating whether rate limiting is enabled for these routes.
    /// </value>
    public bool EnableRateLimiting { get; set; }

    /// <summary>
    /// Gets or sets the list of white listed clients.
    /// </summary>
    /// <value>
    /// A <see cref="List{T}"/> of client identifiers that are exempt from rate limiting.
    /// </value>
    public List<string> ClientWhitelist { get; set; } = new();

    /// <summary>
    /// Gets or sets the period, e.g. 1s, 1m, 1h.
    /// </summary>
    /// <value>
    /// A string representing the time period for rate limiting, e.g. "1s", "1m", "1h".
    /// </value>
    public string Period { get; set; }

    /// <summary>
    /// Gets or sets the period timespan - how long we should wait before resetting counters in seconds.
    /// </summary>
    /// <value>
    /// A double value representing the number of seconds to wait before resetting counters.
    /// </value>
    public double PeriodTimespan { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of requests that a client can make in a defined period.
    /// </summary>
    /// <value>
    /// A long value representing the maximum number of allowed requests within the specified period.
    /// </value>
    public long Limit { get; set; }
}
