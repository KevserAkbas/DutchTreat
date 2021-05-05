using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]

    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger _logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Get() //tüm koleysiyonu alma
        {
            try
            {
                return Ok(_repository.GetAllOrders());
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
                var order = _repository.GetOrderById(id);
                if (order != null)
                {
                    return Ok(order);
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
        public IActionResult Post([FromBody] Order model)
        {
            try
            {
                _repository.AddEntity(model);
                if (_repository.SaveAll()) //SaveAll() - degişiklikleri kaydetmesine izin vermek için
                {
                    return Created($"/api/orders/{model.Id}", model); //Created - 201 döndürür
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
