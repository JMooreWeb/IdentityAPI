using JMooreWeb.API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JMooreWeb.API.Data
{
	public class Seed
	{
		public static void SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
		{
			if (!userManager.Users.Any())
			{
				var userData = File.ReadAllText("Data/UserSeedData.json");
				var users = JsonConvert.DeserializeObject<List<User>>(userData);

				var roles = new List<Role>
				{
					new Role { Name = "Member" },
					new Role { Name = "Admin" },
					new Role { Name = "Moderator" },
					new Role { Name = "VIP" }
				};

				foreach (var role in roles)
				{
					roleManager.CreateAsync(role).Wait();
				}

				foreach (var user in users)
				{
					userManager.CreateAsync(user, "password").Wait();
					userManager.AddToRoleAsync(user, "Member").Wait();
				}

				var adminUser = new User
				{
					UserName = "Admin"
				};

				IdentityResult result = userManager.CreateAsync(adminUser, "password").Result;

				if (result.Succeeded)
				{
					var admin = userManager.FindByNameAsync("Admin").Result;
					userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" }).Wait();
				}
			}
		}
	}
}
