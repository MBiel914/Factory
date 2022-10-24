using System.ComponentModel.DataAnnotations;
using Factory.API.Service.Configurations;
using Microsoft.AspNetCore.Identity;

namespace Factory.API.Service.DTOs.User
{
    public record ApiUserDto : LoginDto, IMapable<ApiUserDto, IdentityUser>
    {
        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        public IdentityUser Map(ApiUserDto source = null)
        {
            return new IdentityUser
            {
                Email = this.Email,
                UserName = this.Email
            };
        }
    }
}
