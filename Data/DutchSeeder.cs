using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext ctx, IWebHostEnvironment env, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _env = env;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("kevser@dt.com");
            if (user == null) //kullanıcı yoksa onu oluşturmamız gerekir
            {
                user = new StoreUser()
                {
                    FirstName = "Kevser",
                    LastNamr = "Akbaş",
                    Email = "kevser@dt.com",
                    UserName = "kevser@dt.com"
                };

                var result = await _userManager.CreateAsync(user, "X12345x.");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Yeni bir kullanıcı oluşturulamadı");
                }
            }

            if (!_ctx.Products.Any())
            {

                var filePath = Path.Combine(_env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);
                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                             Product=products.First(),
                             Quantity=5,
                             UnitPrice=products.First().Price
                        }
                    };
                }
                //var order = new Order()
                //{
                //    OrderDate = DateTime.Today,
                //    OrderNumber = "1000",
                //    Items = new List<OrderItem>()
                //    {
                //        new OrderItem()
                //        {
                //            Product=products.First(),
                //            Quantity=5,
                //            UnitPrice=products.First().Price
                //        }
                //    }
                //};
                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}
