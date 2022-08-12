using System.Collections.Generic;
using DomainLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        Product AddNewProduct(Product product);
        Product UpdateProduct(int id, Product product);
        bool DeleteProduct(int id);
    
    }
}
