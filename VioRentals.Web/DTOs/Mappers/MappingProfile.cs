using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VioRentals.Core.Entities;

namespace VioRentals.Web.DTOs.Mappers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserDto, UserEntity>()
				.ForMember(u => u.Id, opt => opt.Ignore());

			CreateMap<CustomerDto, CustomerEntity>();

			CreateMap<CustomerEntity, CustomerDto>();

			CreateMap<MovieDto, MovieEntity>();

			CreateMap<MovieEntity, MovieDto>();
		}
	}
}
