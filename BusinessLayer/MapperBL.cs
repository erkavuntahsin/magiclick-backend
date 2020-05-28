using AutoMapper;
using DataAccessLayer.Models;
using Entities.MApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
   public class MapperBL
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {

                config.CreateMap<Category, CategoryDO>()
                .ForMember(dest => dest.ProductDO, opt => opt.ResolveUsing(fa => fa.Product))
                 .ReverseMap();

                config.CreateMap<Product, ProductDO>()
                 .ForMember(dest => dest.CategoryDO, opt => opt.ResolveUsing(fa => fa.Category))
                 .ReverseMap();
            });
        }
    }
}
