using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchContext> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchContext> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
            }
            else
            {
                return _ctx.Orders
               .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersbyUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
            else
            {
                return _ctx.Orders
                    .Where(o => o.User.UserName == username)
                .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts() //tüm ürünlerin listesini alacaktır.
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called ");

                return _ctx.Products
                        .OrderBy(p => p.Title)
                        .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }

        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id)
                .FirstOrDefault();
        }

        public Order GetOrderById(string username, int id)
        {
            return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.User.UserName == username)
                .FirstOrDefault();
        }

        // bu API'nin kullanıcısından bizi istedikleri kategoride geçirmesini isteyeceğiz
        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }
        public bool SaveAll()
        {
            //SaveChanges etkilenen satır sayısını döndürür.
            return _ctx.SaveChanges() > 0;
        }

    }
}
