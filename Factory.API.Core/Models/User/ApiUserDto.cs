﻿using System.ComponentModel.DataAnnotations;

namespace Factory.API.Core.Models.User
{
    public class ApiUserDto : LoginDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}