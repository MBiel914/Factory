using Factory.API.Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Factory.API.Core.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDto user);
        Task<IEnumerable<IdentityError>> RegisterAdministrator(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<string> CreateRefreshToken();
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
