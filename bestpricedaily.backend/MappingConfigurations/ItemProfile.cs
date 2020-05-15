using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bestpricedaily.Models;
using bestpricedaily.ViewModels;

namespace tst.MappingConfigurations
{
    public class ItemProfile : Profile
    {
     
            public ItemProfile()
            {
                // Default mapping when property names are same
                CreateMap<Item, ItemViewModel>();

                // Mapping when property names are different
                //CreateMap<User, UserViewModel>()
                //    .ForMember(dest =>
                //    dest.FName,
                //    opt => opt.MapFrom(src => src.FirstName))
                //    .ForMember(dest =>
                //    dest.LName,
                //    opt => opt.MapFrom(src => src.LastName));
            }
    }
}
