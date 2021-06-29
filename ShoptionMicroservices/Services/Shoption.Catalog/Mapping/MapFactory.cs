using System;
using AutoMapper;
using Shoption.Catalog.Dto;
using Shoption.Catalog.Models;

namespace Shoption.Catalog.Mapping
{
    public class MapFactory:Profile
    {
        public MapFactory()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }   
    }
}
