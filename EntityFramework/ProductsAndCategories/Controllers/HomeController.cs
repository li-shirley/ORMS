using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private ProductsAndCategoriesContext db;
        public HomeController(ProductsAndCategoriesContext context)
        {
            db = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Product> products = db.Products
            .OrderBy(p => p.Name)
            .ToList();
            ViewBag.Products = products;
            return View("Index");
        }

        [HttpPost("products/create")]
        public IActionResult CreateProduct(Product newProduct)
        {
            List<Product> products = db.Products
            .OrderBy(p => p.Name)
            .ToList();
            ViewBag.Products = products;

            if (ModelState.IsValid)
            {
                if (db.Products.Any(p => p.Name == newProduct.Name))
                {
                    ModelState.AddModelError("Name", "Product already exists.");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            db.Products.Add(newProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("products/{productId}")]
        public IActionResult Product(int productId)
        {
            Product product = db.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(c => c.Category)
                .FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ProductWithSelectedCategories = product;

            List<Category> categories = db.Categories
                .Include(c => c.ProductCategories)
                .Where(c => c.ProductCategories.Any(pc => pc.ProductId == product.ProductId) == false)
                .ToList();
            ViewBag.UnselectedCategories = categories;

            return View("Product");
        }

        [HttpPost("/products/{productId}/addcategory")]
        public IActionResult ProductAddCategory(int productId, ProductCategory newProductCategory)
        {
            db.ProductCategories.Add(newProductCategory);
            db.SaveChanges(); 
            return RedirectToAction("Product", new { productId = productId }); 
        }

    


        [HttpGet("/categories")]
        public IActionResult Categories()
        {
            List<Category> categories = db.Categories
            .OrderBy(c => c.Name)
            .ToList();
            ViewBag.Categories = categories;
            return View("Categories");
        }

        [HttpPost("categories/create")]
        public IActionResult CreateCategory(Category newCategory)
        {
            List<Category> categories = db.Categories
            .OrderBy(c => c.Name)
            .ToList();
            ViewBag.Categories = categories;

            if (ModelState.IsValid)
            {
                if (db.Categories.Any(c => c.Name == newCategory.Name))
                {
                    ModelState.AddModelError("Name", "Category already exists.");
                }
            }
            if (ModelState.IsValid == false)
            {
                return View("Categories");
            }
            db.Categories.Add(newCategory);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpGet("categories/{categoryId}")]
        public IActionResult Category(int categoryId)
        {
            Category category = db.Categories
                .Include(c => c.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return RedirectToAction("Categories");
            }
            ViewBag.CategoryWithSelectedProducts = category;

            List<Product> products = db.Products
                .Include(p => p.ProductCategories)
                .Where(p => p.ProductCategories.Any(pc => pc.CategoryId == category.CategoryId) == false)
                .ToList();
            ViewBag.UnselectedProducts = products;

            return View("Category");
        }

        [HttpPost("/categories/{categoryId}/addproduct")]
        public IActionResult CategoryAddProduct(int categoryId, ProductCategory newProductCategory)
        {
            db.ProductCategories.Add(newProductCategory);
            db.SaveChanges(); 
            return RedirectToAction("Category", new { categoryId = categoryId }); 
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
