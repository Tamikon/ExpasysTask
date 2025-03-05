using ASP_Homework_Product.Models;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Delete(int id);
        List<Product> GetProducts();
        Product TryGetById(int id);
        void Update(Product product);
    }
}