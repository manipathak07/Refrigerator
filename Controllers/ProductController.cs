using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refrigrator.Data;
using Refrigrator.Models;
using System.Linq;

namespace Refrigrator.Controllers
{
    public class ProductController : Controller
    {

        private AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
        //    // Add sample data
        //    products.Add(new Product { Id = 1, Name = "Milk", Quantity = 2.5m, LastUpdated = DateTime.Now.AddDays(-2), ExpirationDate = DateTime.Now.AddDays(7) });
        //    products.Add(new Product { Id = 2, Name = "Eggs", Quantity = 12, LastUpdated = DateTime.Now.AddDays(-1), ExpirationDate = DateTime.Now.AddDays(-10) });
        //    products.Add(new Product { Id = 3, Name = "Cheese", Quantity = 0.5m, LastUpdated = DateTime.Now.AddDays(-3), ExpirationDate = DateTime.Now.AddDays(5) });
        //    products.Add(new Product { Id = 4, Name = "Yogurt", Quantity = 1, LastUpdated = DateTime.Now.AddDays(-5), ExpirationDate = DateTime.Now.AddDays(3) });


            _dbContext = dbContext; 
        }


        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> expiredProducts = _dbContext.Products
                        .Where(p => p.ExpirationDate > DateTime.Now)
                        .ToList();
            int totalQuantity = (int)expiredProducts.Sum(q => q.Quantity);

            ViewBag.TotalQuantity = totalQuantity;
            int remainingquantity = 480 - totalQuantity;
            ViewBag.RemainingQuantity = remainingquantity;          

            if (expiredProducts.Any())
            {
                ViewBag.ExpiredProducts = expiredProducts;
            }          


            List<Product> products = _dbContext.Products.ToList();

            return View(products);
        }
            

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {

            List<Product> expiredProducts = _dbContext.Products
                      .Where(p => p.ExpirationDate > DateTime.Now)
                      .ToList();
            int totalQuantity = (int)expiredProducts.Sum(q => q.Quantity);                    
            
                ViewBag.TotalQuantity = totalQuantity;
                int remainingquantity = 480 - totalQuantity;
                ViewBag.RemainingQuantity = remainingquantity;
                int quantityCheck = remainingquantity - product.Quantity;

            
            // Save the new product
            if (ModelState.IsValid && quantityCheck >= 0)
            {
                using (var dbContext = new AppDbContext())
                {
                    dbContext.Products.Add(product);
                    dbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
         
                TempData["AlertMessage"] = remainingquantity;
            

            return View(product);
          
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the product from the database using the provided id
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // Handle the case when the product is not found
            }

            return View(product); // Pass the product to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Retrieve the product from the database using the provided id
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // Handle the case when the product is not found
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction("Index"); // Redirect to the desired view after deletion
        }

    }
}
