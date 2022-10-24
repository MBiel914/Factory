using System.ComponentModel.DataAnnotations;

namespace Factory.API.Service.DTOs.User
{
    public record LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [StringLength(15, ErrorMessage = "Your Password is limited to {2} to {0} characters", MinimumLength = 6)]
        public string Password { get; init; }
    }
}
