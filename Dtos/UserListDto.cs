using System;

namespace JMooreWeb.API.Dtos
{
	public class UserListDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastActive { get; set; }
	}
}
