using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Supplier { get; set; }
        public string Price { get; set; }


        public Product()
        {
        }


        public Product(int productID, string name, string description, string category, string manufacturer, string supplier, string price)
        {
            ProductID = productID;
            Name = name;
            Description = description;
            Category = category;
            Manufacturer = manufacturer;
            Supplier = supplier;
            Price = price;
        }


    }
}
