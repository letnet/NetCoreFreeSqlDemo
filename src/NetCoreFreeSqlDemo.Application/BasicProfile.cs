using AutoMapper;
using NetCoreFreeSqlDemo.Application.Models;
using NetCoreFreeSqlDemo.Application.Entitys;
using NetCoreFreeSqlDemo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFreeSqlDemo.Application
{
    public class BasicProfile : Profile
    {
        public BasicProfile()
        {
            CreateMap<Test, TestDto>().ReverseMap();
        }
    }
}


