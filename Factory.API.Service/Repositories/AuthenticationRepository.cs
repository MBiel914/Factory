using Factory.API.Service.Contracts;
using Factory.API.Service.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Factory.API.Service.Repositories
{
    public class AuthenticationRepository : IAuthManager
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private IdentityUser? _user;

        private const string _loginProvider = "FactoryAPI";
        private const string _refreshToken = "RefreshToken";

        public AuthenticationRepository(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);

            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

            return newRefreshToken;
        }

        public async Task<AuthResponseDto?> Login(LoginDto loginDto)
        {
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user is null || isValidUser == false)
            {
                return null;
            }

            var token = await GenerateTokenAsync();

            return new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<IEnumerable<IdentityError>> RegisterUser(ApiUserDto userDto)
        {
            _user = userDto.Map();

            var result = await _userManager.CreateAsync(_user, userDto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAdministrator(ApiUserDto userDto)
        {
            _user = userDto.Map();

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "Administrator");
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSerurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSerurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;

            _user = await _userManager.FindByNameAsync(username);
            if (_user is null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateTokenAsync();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> GenerateTokenAsync()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = _userManager.GetRolesAsync(_user);
            var userClaims = _userManager.GetClaimsAsync(_user);

            await Task.WhenAll(roles, userClaims);

            var roleClaims = (await roles).Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id)
            }
            .Union((await userClaims)).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
