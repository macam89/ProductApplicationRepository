using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interfaces;
using DomainLayer.Models;

namespace DataAccessLayer.DBRepositories
{
    public class ProductDBRepository : IProductRepository
    {
        public IEnumerable<Product> GetAllProducts()
        {
            using (var db = new AppDBContext.AppContext())
            {
                return db.Product.ToList();
            }
        }


        public Product GetProduct(int id)
        {
            using (var db = new AppDBContext.AppContext())
            {
                var product = db.Product.Find(id);

                return product;
            }
        }


        public Product AddNewProduct(Product product)
        {
            using (var db = new AppDBContext.AppContext())
            {
                var existingProduct = db.Product.Where(b => b.Name.ToUpper() == product.Name.ToUpper()).FirstOrDefault();

                if (existingProduct != null)
                {
                    return null;
                }

                db.Product.Add(product);
                db.SaveChanges();

                return product;
            }
        }


        public Product UpdateProduct(int id, Product product)
        {
            using (var db = new AppDBContext.AppContext())
            {
                var existingProduct = db.Product.Find(id);

                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Category = product.Category;
                    existingProduct.Manufacturer = product.Manufacturer;
                    existingProduct.Supplier = product.Supplier;
                    existingProduct.Price = product.Price;

                    db.Product.Update(existingProduct);
                    db.SaveChanges();

                    return existingProduct;
                }
                else
                {
                    return null;
                }
            }
        }


        public bool DeleteProduct(int id)
        {
            using (var db = new AppDBContext.AppContext())
            {
                var product = db.Product.Find(id);

                if (product != null)
                {
                    db.Product.Remove(product);
                    db.SaveChanges();

                    return true;
                }

                return false;
            }
        }


    }
}
