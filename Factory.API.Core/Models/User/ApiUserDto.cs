using System.ComponentModel.DataAnnotations;
using Factory.API.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Factory.API.Core.Models.User
{
    public class ApiUserDto : LoginDto, IMapable<ApiUserDto, IdentityUser>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

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
