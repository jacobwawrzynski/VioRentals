using AutoMapper;
using VioRentals.Core.DTOs;
using VioRentals.Core.Entities;

namespace VioRentals.Core.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<UserEntity, AuthenticateUserDto>();

            // RegisterRequest -> User
            CreateMap<RegisterDto, UserEntity>();

            // UpdateRequest -> User
            CreateMap<RegisterDto, UserEntity>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
