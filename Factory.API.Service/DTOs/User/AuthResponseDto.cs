namespace Factory.API.Service.DTOs.User
{
    public record AuthResponseDto
    {
        public string UserId { get; init; }
        public string Token { get; init; }
        public string RefreshToken { get; init; }
    }
}
