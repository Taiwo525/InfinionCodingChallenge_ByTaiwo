using AutoMapper;
using InfinionProduct_Core.DTOs.ProductDTOs;
using InfinionProduct_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinionProduct_Application.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<ProductDto, CreateProductDto>().ReverseMap();

        }
    }
}
