using JMooreWeb.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JMooreWeb.API.Data
{
	public class DataContext : IdentityDbContext<User, Role, int,
		IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
		IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>(e => e.ToTable("Users"));
			builder.Entity<Role>(e => e.ToTable("Roles"));
			builder.Entity<UserRole>(e => e.ToTable("UserRoles"));

			builder.Entity<IdentityUserClaim<int>>(e => e.ToTable("UserClaims"));
			builder.Entity<IdentityUserLogin<int>>(e => e.ToTable("UserLogins"));
			builder.Entity<IdentityUserToken<int>>(e => e.ToTable("UserTokens"));
			builder.Entity<IdentityRoleClaim<int>>(e => e.ToTable("RoleClaims"));
		}
	}

}
