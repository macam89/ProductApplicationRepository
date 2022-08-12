using System.Collections.Generic;
using DomainLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Dictionary<string, Product> AddNewProduct(Product product);
        Dictionary<string, Product> UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
    }
}
