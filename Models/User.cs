using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JMooreWeb.API.Models
{
	public class User : IdentityUser<int>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
