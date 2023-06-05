using AutoMapper;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Web.DTOs.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserDto, UserEntity>()
				.ForMember(user => user.Id, opt => opt.Ignore());
		}
	}
}
