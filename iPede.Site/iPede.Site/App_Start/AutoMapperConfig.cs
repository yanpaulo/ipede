using AutoMapper;
using iPede.Site.Models.DTOs;
using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPede.Site
{
    public class AutoMapperConfig
    {
        private static MapperConfiguration mapper;

        public static void Configure()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(source => source.MainOrNoImage.Url));

                cfg.CreateMap<Category, CategoryDTO>();
            });
        }

        public static MapperConfiguration GetMapperInstance()
        {
            return mapper;
        }
    }
}