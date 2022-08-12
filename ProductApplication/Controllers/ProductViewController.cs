using System.Collections.Generic;
using System.Linq;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;


namespace ProductApplication.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProductViewController : Controller
    {
        private readonly IProductService _products;


        public ProductViewController(IProductService products)
        {
            _products = products;
        }


        [Route("~/Products")]
        public ActionResult Products()
        {
            var products = _products.GetAllProducts();
            
            return View(products.ToList());
        }


        [Route("~/Products/{id}")]
        public ActionResult Details(int id)
        {
            var product = _products.GetProduct(id);
            return View(product);
        }


        [Route("~/Products/Create")]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("~/Products/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            var resultDictionary = _products.AddNewProduct(product);

            if (resultDictionary.ContainsKey("NameAlreadyExist"))
            {
                ModelState.AddModelError(string.Empty, "Name already exist.");
                return View();
            }
            else
            {
                Product newProduct = resultDictionary.GetValueOrDefault("Created");
                return RedirectToAction("Details", "ProductView", new { id = newProduct.ProductID });
            }
        }


        [Route("~/Products/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        [Route("~/Products/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            var resultDictionary = _products.UpdateProduct(id, product);

            if (resultDictionary.ContainsKey("NotFound"))
            {
                ModelState.AddModelError(string.Empty, "Product not found.");
                return View();
            }
            else if (resultDictionary.ContainsKey("NameAlreadyExist"))
            {
                ModelState.AddModelError(string.Empty, "That product name aleady exist.");
                return View();
            }
            else
            {
                return RedirectToAction("Details", "ProductView", new { id });
            }
        }


        [Route("~/Products/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            bool deleted = _products.DeleteProduct(id);

            if (deleted)
            {
                return RedirectToAction("Products");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Product not exist.");
                return View();
            }
        }


    }
}
