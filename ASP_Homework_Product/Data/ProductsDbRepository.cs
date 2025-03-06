using ASP_Homework_Product.Data;
using ASP_Homework_Product.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ASP_Homework_Product
{
    public class ProductsDbRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductsDbRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product TryGetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Cost = product.Cost;
                existingProduct.ImagePath = product.ImagePath;
                _context.SaveChanges();
            }
        }
    }
}