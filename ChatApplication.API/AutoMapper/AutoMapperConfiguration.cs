using AutoMapper;
using ChatApplication.API.Models;
using ChatApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.AutoMapper
{
    public class AutoMapperConfiguration:Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, UserAuthenticationResponseModel>();
        }
    }
}
