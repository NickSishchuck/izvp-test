using System.Diagnostics.CodeAnalysis;

namespace TestApi.DTOs.Requests
{
    [ExcludeFromCodeCoverage]
    public record AdminLoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
