using JMooreWeb.API.Models;
using System.Threading.Tasks;

namespace JMooreWeb.API.Services
{
	public interface IAuthRepository
	{
		Task<User> Register(User user, string password);
		Task<User> Login(string username, string password);
		Task<bool> UserExists(string username);
	}
}
