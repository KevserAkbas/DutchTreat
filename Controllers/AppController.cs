﻿using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
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
        private readonly IMailService _mailService;
        private readonly DutchContext _cxt;

        public AppController(IMailService mailService, DutchContext cxt)
        {
            _mailService = mailService;
            _cxt = cxt; //basitçe bir alan oluşturup onu depolayacak
        }

        //Controller, belirli bir eyleme gelen bir isteği eşlememize izin verir ve
        //bu eylem gerçek mantığın gerçekleşeceği yerdir.

        // IActionResult --> arabirim döndüren yöntemlerden biri
        // IActionResult --> burada olanları almanın, onu bir view a eşleştirmenin ve
        // nihayetinde geri getirmenin yollarından biridir. 
        public IActionResult Index()
        {
            var results = _cxt.Products.ToList();
            return View();
            //throw new InvalidProgramException("Bad things happen to good developers");
        }

        //1-Bu bir get işleminde çağırılacak ki bu da bir sayfayı almanın basit bir yoludur.
        [HttpGet("contact")]
        public IActionResult Contact()
        {

            //throw new InvalidProgramException("Bad things happen");
            return View();
        }

        //2-Ancak tarayıcı bize bilgileri geri göndermek istediğinde,
        //bunu bir post şeklinde yapacak ve sonra bu kod parçası çağırılacak
        [HttpPost("contact")] //MVC de bunlar eşleştiğinde ne tür bir talep geldiğini söylüyor.
        public IActionResult Contact(ContactViewModel model)
        {
            //input ve text alanlarının adları(name) veya formda kullandığımız alanı
            //ContactViewModel in özelliklerine eşleyecektir.

            //if-else yapısı ile sunucuya veri getirildiğinde,
            //viewModel e koyululan kuralların gerçekten uygulandığından emin olma yoludur 
            if (ModelState.IsValid)
            { //çağrı geldiğinde ModelState in geçerli olup olmadığını kontrol eder
                _mailService.SendMessage("kevserakbas24@hotmail.com", model.Subject, $"From:{model.Name} - {model.Email},Message:{model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();//modeli-formu temizlemek için
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        public IActionResult Shop()// Kullanıcılar için alışveriş sayfası
        { //tüm ürünler için veritabanını sorgulayıp mağaza sayfasına göndermek için
          //DutchContext sınıfına erişmek gerekir bunu da yukarıda constructor da
          //(AppController) da yapılır

            //var results = _context.Products
            //    .OrderBy(p=>p.Category)//sorgulama / kategoriye göre sıralama - bunu bir bağlantı sorgusuyla da yapabilirz
            //    .ToList(); //veritabanına gidecek, tüm ürünleri alacak ve iade edecek.
            var results = from p in _context.Products
                          orderby p.Category
                          select p;
            return View(results.ToList());//verileri view  a aktarıyoruz
        }
    }
}
