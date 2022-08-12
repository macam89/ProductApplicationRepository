using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DataAccessLayer.Interfaces;
using DomainLayer.Models;

namespace DataAccessLayer.FileRepositories
{
    public class ProductFileRepository : IProductRepository
    {

        public static string currentDirectory = Directory.GetCurrentDirectory().ToString();
        public static string parentDirectory = Directory.GetParent(currentDirectory).ToString();
        public static string file = parentDirectory + "/DataAccessLayer/JsonData/productFile.json";


        public IEnumerable<Product> GetAllProducts()
        {
            return ReadFile();
        }


        public Product GetProduct(int id)
        {
            List<Product> ListOfProducts = ReadFile();

            var product = ListOfProducts.FirstOrDefault(m => m.ProductID == id);

            return product;
        }


        public Product AddNewProduct(Product product)
        {
            List<Product> ListOfProducts = ReadFile();

            var existingProduct = ListOfProducts.Where(b => b.Name.ToUpper() == product.Name.ToUpper()).FirstOrDefault();

            if (existingProduct != null)
            {
                return null;
            }

            var newProduct = AddProduct(product, ListOfProducts);
            
            return newProduct;
        }


        private static Product AddProduct(Product product, List<Product> ListOfProducts)
        {
            var last = ListOfProducts[ListOfProducts.Count - 1];
            int id = last.ProductID + 1;
            var newProduct = new Product(
                id,
                product.Name,
                product.Description,
                product.Category,
                product.Manufacturer,
                product.Supplier,
                product.Price
                );

            ListOfProducts.Add(newProduct);

            WriteToFile(ListOfProducts);

            ListOfProducts = ReadFile();

            newProduct = ListOfProducts.FirstOrDefault(m => m.ProductID == id);

            return newProduct;
        }


        public Product UpdateProduct(int id, Product product)
        {
            List<Product> ListOfProducts = ReadFile();

            var existingProduct = ListOfProducts.Where(c => c.ProductID == id).FirstOrDefault();

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Category = product.Category;
                existingProduct.Manufacturer = product.Manufacturer;
                existingProduct.Supplier = product.Supplier;
                existingProduct.Price = product.Price;

                WriteToFile(ListOfProducts);

                ListOfProducts = ReadFile();

                existingProduct = ListOfProducts.FirstOrDefault(m => m.ProductID == id);

                return existingProduct;
            }
            else
            {
                return null;
            }
        }


        public bool DeleteProduct(int id)
        {
            List<Product> ListOfProducts = ReadFile();

            var product = ListOfProducts.Where(m => m.ProductID == id).FirstOrDefault();

            if (product != null)
            {
                ListOfProducts.Remove(product);

                WriteToFile(ListOfProducts);

                return true;
            }

            return false;
        }


        public static void WriteToFile(List<Product> listOfProducts)
        {
            string jsonString = JsonSerializer.Serialize(listOfProducts, new JsonSerializerOptions() { WriteIndented = true });

            using (StreamWriter w = new StreamWriter(file))
            {
                w.WriteLine(jsonString);
            }
        }


        public static List<Product> ReadFile()
        {
            List<Product> ListOfProducts = new List<Product>();

            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                ListOfProducts = JsonSerializer.Deserialize<List<Product>>(json);
            }

            return ListOfProducts;
        }


    }
}
