using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DomainLayer.Models;
using ServiceLayer.Interfaces;


namespace ServiceLayer.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;


        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }


        public Product GetProduct(int id)
        {
            return _repository.GetProduct(id);
        }


        public Dictionary<string, Product> AddNewProduct(Product product)
        {
            string response;
            var resultDictionary = new Dictionary<string, Product>();

            var newProduct = _repository.AddNewProduct(product);

            if (newProduct == null)
            {
                response = ProductEnums.NameAlreadyExist.ToString();
                resultDictionary.Add(response, null);
                return resultDictionary;
            }

            response = ProductEnums.Created.ToString();
            resultDictionary.Add(response, newProduct);
            return resultDictionary;
        }


        public Dictionary<string, Product> UpdateProduct(int id, Product product)
        {
            var existingProduct = _repository.GetProduct(id);

            string response;
            var resultDictionary = new Dictionary<string, Product>();

            if (existingProduct == null)
            {
                response = ProductEnums.NotFound.ToString();
                resultDictionary.Add(response, null);
                return resultDictionary;
            }

            var productsExceptUpdatingProduct = _repository.GetAllProducts().Where(c => c.ProductID != id);
            var productWithSameName = productsExceptUpdatingProduct.Where(m => m.Name.ToUpper() == product.Name.ToUpper()).FirstOrDefault();

            if (productWithSameName != null)
            {
                response = ProductEnums.NameAlreadyExist.ToString();
                resultDictionary.Add(response, null);
                return resultDictionary;
            }

            existingProduct = _repository.UpdateProduct(id, product);

            if (existingProduct != null)
            {
                response = ProductEnums.NoContent.ToString();
                resultDictionary.Add(response, existingProduct);
                return resultDictionary;
            }
            else
            {
                response = ProductEnums.NotFound.ToString();
                resultDictionary.Add(response, null);
                return resultDictionary;
            }
        }


        public bool DeleteProduct(int id)
        {
            return _repository.DeleteProduct(id);
        }


        public enum ProductEnums
        {
            NameAlreadyExist,
            Created,
            NoContent,
            NotFound
        }


    }
}
