using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Product> GetAllProducts() //tüm ürünlerin listesini alacaktır.
        {
            return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
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

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
