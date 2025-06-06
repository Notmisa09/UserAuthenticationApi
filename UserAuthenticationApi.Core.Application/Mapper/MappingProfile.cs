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
            CreateMap<PhoneReqAddDto, Phone>().ReverseMap();

            CreateMap<Users, UsersDto>().ReverseMap();

            CreateMap<UserResAddDto, Users>().ReverseMap();

            CreateMap<AddUsersCommand, Users>().ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.Phones))
                .AfterMap((src, dest) => {
                    foreach(var phone in dest.Phones)
                    {
                        phone.Users.Id = dest.Id;
                    }
            }).ReverseMap();
        }
    }
}
