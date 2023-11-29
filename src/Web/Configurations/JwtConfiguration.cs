namespace Api.Configurations
{
    internal class JwtConfiguration
    {
        public required string Issuer { get; init; }
        public required string Audience { get; init; }
        public required string Key { get; init; }
    }
}
