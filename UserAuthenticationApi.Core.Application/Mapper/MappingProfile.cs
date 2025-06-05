using AutoMapper;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapeo de phoneDto a Entidad base
            CreateMap<PhonesDto, Phone>().ReverseMap();
            CreateMap<PhoneAddDto, Phone>().ReverseMap();

            CreateMap<Users, UsersDto>().ReverseMap();

            //Mapeo de addUserCommand a entidad base
            CreateMap<AddUsersCommand, Users>().ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones)).ReverseMap();
        }
    }
}
