using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    //Bu sınıf MVC içindeki Controller Adlı bir sınıftan türetilecek
    public class AppController : Controller
    {
        //Controller, belirli bir eyleme gelen bir isteği eşlememize izin verir ve
        //bu eylem gerçek mantığın gerçekleşeceği yerdir.

        // IActionResult --> arabirim döndüren yöntemlerden biri
        // IActionResult --> burada olanları almanın, onu bir view a eşleştirmenin ve
        // nihayetinde geri getirmenin yollarından biridir. 
        public IActionResult Index()
        {
            return View();
        }
    }
}
