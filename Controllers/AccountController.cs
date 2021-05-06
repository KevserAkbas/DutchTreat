using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AccountController:Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)//birisi zaten giriş yapmış mı 
            {//zaten oturum açılmışsa, oturum açma sayfası kullanışlı değildir
                //bu nedenle insanların yanlışlıkla giriş yapmamalarını ve kafalarının karışmamasını sağlama için
                return RedirectToAction("Index", "App");
            }
            return View();
        }
    }
}
