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
    [ApiController]
    [Produces("application/json")]//ApiController ın her zaman applicaion/json ı döndüreceğini söyler
    public class ProductsController : ControllerBase      
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]// ProducesResponseType - beklenen tüm farklı yanıt türlerini anlatmak için
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
                //Ok - dönüşte hangi durum kodunu döndürmek istediğimizi söylüyoruz
                //Ok 200 dür
                //Doğru veriler alıyoruz ve kötü bir şey olmuyor
            }
            catch (Exception ex)
            {

                 _logger.LogError($"Failed to get products:{ex}");
                return BadRequest("Failed to get products");
            }
            
        }
    }
}
