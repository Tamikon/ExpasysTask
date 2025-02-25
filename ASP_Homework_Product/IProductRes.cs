using ASP_Homework_Product.Models;
using System.Collections.Generic;

namespace ASP_Homework_Product
{
    public interface IProductRes
    {
        void Add(Product product);
        List<Product> GetProducts();
        Product TryGetById(int id);
        void Update(Product product);
    }
}