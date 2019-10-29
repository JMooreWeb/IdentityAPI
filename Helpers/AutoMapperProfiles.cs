using AutoMapper;
using JMooreWeb.API.Dtos;
using JMooreWeb.API.Models;

namespace JMooreWeb.API.Helpers
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, UserListDto>();
			CreateMap<UserRegisterDto, User>();

			//CreateMap<User, UserDetailedDto>();
		}
	}
}
