using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthenticationApi.Core.Application.Dtos;
using UserAuthenticationApi.Core.Application.Feautures.Users.Commands;
using UserAuthenticationApi.Core.Domain.Entities;

namespace UserAuthenticationApi.Core.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mapeo de phoneDto a Entidad base
            CreateMap<PhonesDto, Phone>().ReverseMap();

            CreateMap<AddUsersCommand, Users>().ReverseMap();
        }
    }
}
