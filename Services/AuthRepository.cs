using JMooreWeb.API.Data;
using JMooreWeb.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JMooreWeb.API.Services
{
	public class AuthRepository : IAuthRepository
	{
		private readonly DataContext _context;

		public AuthRepository(DataContext context)
		{
			_context = context;
		}
		public async Task<User> Login(string username, string password)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

			if (user == null)
				return null;

			return user;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		public async Task<User> Register(User user, string password)
		{
			byte[] passwordHash, passwordSalt;
			CreatePasswordHash(password, out passwordHash, out passwordSalt);

			await _context.Users.AddAsync(user);

			await _context.SaveChangesAsync();

			return user;
		}

		public async Task<bool> UserExists(string username)
		{
			if (await _context.Users.AnyAsync(x => x.UserName == username))
				return true;

			return false;
		}
	}
}
