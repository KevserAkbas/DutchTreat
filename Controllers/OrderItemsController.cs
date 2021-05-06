using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    //Burada asıl olay, öğeleri (Items) bireysel aramalarıyle eşleştirmemizdir
    //Rotayı Controller için ayarlama
    [Route("/api/orders/{orderid}/items")] //URL ayarlaması
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository repository,
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId) //orderId - her aramada kullanacağımız parametre
                                              //URL de de parametre olarak bulunmaktadır
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                //IEnumareble<OrderItem> den IEnumareble<OrderItemViewModel> e eşleme yapılır
                return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            return NotFound();
        }

        //örneğin http://localhost:8888/api/orders/1/items/1 dersek burada
        //ilk siparişi ve ardından bu sırada 1 numaralı items ı istediğimizi ima eder
        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)//rotada orderId ye ihtiyaç olduğu için orderId 
        {//tek bir Item ı id sine göre almanın etkili bir yolu
            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item!=null)
                {
                    //IEnumareble<OrderItem> den IEnumareble<OrderItemViewModel> e eşleme yapılır
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));

                }
            }
            return NotFound();

        }
    }
}
