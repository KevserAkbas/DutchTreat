using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchMappingProfile : Profile
    {
        //Profile, kurmak istediğimiz tüm eşlemelerin (mapping) etrafindaki bir kapsayıcıdır
        public DutchMappingProfile()
        {
            //mapping oluşturuldu
            //Order ve OrderViewModel arasında bir harita (map) oluşturuldu
            //ve bu aralarındaki özelliklere bakacak ve özelliği özellik ile eşleştirmeye çalışacak
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))//istisna ayarlama yapıldı aksi halde id ler eşlenemiyo
                .ReverseMap(); //sadece OrderViewModel için bir harita oluşturmakla kalmaz,
                               //aynı zamanda üye eşlemesini alır ve bizim için tersine çevirir.
            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap()
                .ForMember(m=>m.Product,opt=>opt.Ignore());
        
        }
    }
}
