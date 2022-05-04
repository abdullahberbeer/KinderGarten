using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ServerApp.DTO;
using ServerApp.Models;

namespace ServerApp.Helpers
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
           CreateMap<User,UserForListDTO>().ReverseMap();
        }
    }
}