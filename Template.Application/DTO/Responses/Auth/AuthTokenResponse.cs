namespace Template.Application.DTO.Responses.Auth
{
    public record AuthTokenResponse
    {
        public string Token { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
        public DateTime ExpiresIn { get; init; }
        public bool IsFirstAccess { get; init; }
    }
}
