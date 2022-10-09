using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Entities = BasicWebApi.DataAccessLayer.Entity;
using Models = BasicWebApi.Shared.Models;

namespace BasicWebApi.BusinessLayer.MapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            
            CreateMap<Entities.Orders, Models.Order>();
            //.ForMember(dst => dst.Status, opt => opt.MapFrom(source => source.OrderStatus.Name));

            CreateMap<Models.Req.Order.SaveOrder, Entities.Orders>();
        }
    }
}
