using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Responses
{
    /// <summary>
    /// Represents the result of a health check operation, including the status and the time the check was performed.
    /// </summary>
    /// <param name="Status">The health status as a string.</param>
    /// <param name="Timestamp">The date and time, in UTC, when the health check was performed.</param>
    [ExcludeFromCodeCoverage]
    public sealed record HealthStatusResponse(string Status, DateTime Timestamp);
}
