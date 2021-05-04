﻿using DutchTreat.Data.Entities;
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
        public IEnumerable<Product> GetAllProducts() //tüm ürünlerin listesini alacaktır.
        {
            try
            {
                _logger.LogInformation("GetAllProducts was celled ");

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