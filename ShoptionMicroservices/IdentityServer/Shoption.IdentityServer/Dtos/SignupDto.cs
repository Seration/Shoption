﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shoption.IdentityServer.Dtos
{
    public class SignupDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string City { get; set; }
    }
}
