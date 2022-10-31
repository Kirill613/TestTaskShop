using AutoMapper;
using NLayerApp.DAL.Entities;
using System;
using TestTaskShop.Models;

namespace TestTaskShop.Mapper
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Order, EditOrderViewModel>().ReverseMap(); 
            CreateMap<Product, EditProductViewModel>().ReverseMap();            
            CreateMap<Product, AddProductViewModel>().ReverseMap();
        }
    }
}
