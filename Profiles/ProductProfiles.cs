using AutoMapper;
using EcommerceStore.DTOs;
using EcommerceStore.Models;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;

namespace EcommerceStore.Profiles
{
    public class ProductProfiles:Profile
    {
        public ProductProfiles()
        {
            CreateMap<AddProductsDTO, Product>().ReverseMap();
            CreateMap<OrdersDTO, Order>();
            CreateMap<AddUserDTO, User>().ReverseMap();
            //CreateMap<ResponseDTO, Ticket>().ReverseMap();
           // CreateMap<AddUserDto, User>().ReverseMap();
        }
    }
}
