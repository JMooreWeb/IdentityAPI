using AutoMapper;
using JMooreWeb.API.Data;
using JMooreWeb.API.Helpers;
using JMooreWeb.API.Models;
using JMooreWeb.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace JMooreWeb.API
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// Development database - SQLite
		public void ConfigureDevelopmentServices(IServiceCollection services)
		{
			services.AddDbContext<DataContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
			});

			ConfigureServices(services);
		}

		// Production database - SQL Server
		public void ConfigureProductionServices(IServiceCollection services)
		{
			services.AddDbContext<DataContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			ConfigureServices(services);
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			//services.AddDbContext<DataContext>(options =>
			//{
			//	options.UseLazyLoadingProxies();
			//	options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			//});

			IdentityBuilder builder = services.AddIdentityCore<User>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 4;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = true;
			});

			builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
			builder.AddEntityFrameworkStores<DataContext>();
			builder.AddRoleValidator<RoleValidator<Role>>();
			builder.AddRoleManager<RoleManager<Role>>();
			builder.AddSignInManager<SignInManager<User>>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddCors();
			services.AddAutoMapper(typeof(AuthRepository).Assembly);
			services.AddTransient<Seed>();
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<LogUserActivity>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// Global unhandled errors
				app.UseExceptionHandler(builder => {
					builder.Run(async context => {
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

						var error = context.Features.Get<IExceptionHandlerFeature>();
						if (error != null)
						{
							// Custom http response headers
							context.Response.AddApplicationError(error.Error.Message);
							await context.Response.WriteAsync(error.Error.Message);
						}
					});
				});
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
