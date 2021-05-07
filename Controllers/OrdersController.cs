using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper,
            UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]

        public IActionResult Get(bool includeItems = true) //tüm koleysiyonu alma
        {
            try
            {
                var username = User.Identity.Name;
                var result = _repository.GetAllOrdersbyUser(username, includeItems);
                return Ok(_mapper.Map<IEnumerable<OrderViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id) //bireysel sipariş alma id -> hangi siparişi almak istediğimi bilmek için
        {
            try
            {
                var order = _repository.GetOrderById(User.Identity.Name, id);
                if (order != null)
                {
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue) //validation
                    {
                        //orderDate belirtilmediyse 
                        newOrder.OrderDate = DateTime.Now;
                    }

                    //mevcut kullanıcı almak içi
                    var currenUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currenUser;

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll()) //SaveAll() - degişiklikleri kaydetmesine izin vermek için
                    {
                        //Entity i buraya newOrder da ekleyeceğiz,
                        //ama sonra burada 
                        //onu newOrder dan ViewModel e dönüştürmek için başka bir değişiklik yapmamız gerekiyor
                        
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order,OrderViewModel>(newOrder)); //Created - 201 döndürür
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order:{ex}");
            }

            return BadRequest("Failed to save a new order");
        }
    }
}
