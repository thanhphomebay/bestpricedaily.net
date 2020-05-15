using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using QuickType;
using bestpricedaily.Models;
using bestpricedaily.ViewModels;

namespace bestpricedaily.MappingConfigurations
{
    public class OrderProfile : Profile
    {

        public OrderProfile()
        {
            // Default mapping when property names are same
            CreateMap<OrderViewModel, Order>();

            // Mapping when property names are different
            CreateMap<OrderViewModel, Order>()
                .ForMember(dest => dest.Id, opt=>opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.invoice_id, opt => opt.MapFrom(src => src.PurchaseUnits[0].Payments.Captures[0].InvoiceId))
                .ForMember(dest => dest.order_id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.paypal_payment_capture_id, opt => opt.MapFrom(src => src.PurchaseUnits[0].Payments.Captures[0].Id))
            .ForMember(dest => dest.payer_id, opt => opt.MapFrom(src => src.Payer.PayerId))
            .ForMember(dest => dest.surename, opt => opt.MapFrom(src => src.Payer.Name.Surname))
            .ForMember(dest => dest.givenname, opt => opt.MapFrom(src => src.Payer.Name.GivenName))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Payer.EmailAddress))
            .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.PurchaseUnits[0].Payments.Captures[0].Amount.Value));
        }
    }
}
