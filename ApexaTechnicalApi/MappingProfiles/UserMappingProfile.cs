using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexaTechnicalApi.DTOs;
using ApexaTechnicalApi.Models;
using AutoMapper;


namespace ApexaTechnicalApi.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserLoginDto>();
        }
    }

}