﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.DTO
{
	public class CredencialesUsuario
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
