using JMooreWeb.API.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JMooreWeb.API.Helpers
{
	public class LogUserActivity : IAsyncActionFilter
	{
		//public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		//{
		//	var resultContext = await next();

		//	var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
		//	var repo = resultContext.HttpContext.RequestServices.GetService<IAuthRepository>();
		//	var user = await repo.GetUser(userId, true);
		//	user.LastActive = DateTime.Now;

		//	await repo.SaveAll();
		//}

		public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			throw new NotImplementedException();
		}
	}
}
