using System;
using System.ComponentModel.DataAnnotations;

namespace JMooreWeb.API.Dtos
{
	public class UserRegisterDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters")]
		public string Password { get; set; }

		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }

		public UserRegisterDto()
		{
			Created = DateTime.Now;
			LastActive = DateTime.Now;
		}
	}
}
