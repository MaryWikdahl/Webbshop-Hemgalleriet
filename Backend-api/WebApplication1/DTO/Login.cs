﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace WebApplication1.DTO
{
    public class Login
    {
           
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    
}
}
