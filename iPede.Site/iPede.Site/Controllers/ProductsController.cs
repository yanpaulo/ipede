using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPede.Site.Controllers
{
    public class ProductsController : Controller
    {
        private iPedeContext db = new iPedeContext();

        // GET: Products
        public ActionResult Index()
        {
            var categories = db.Categories.Where(item => item.ParentCategoryId == null);
            var products = db.Products.ToList();
            //AssignRandomImages(products.Where(item => item.MainImage == null));
            ViewBag.Categories = categories;
            ViewBag.Categoryname = "Categorias";
            return View(products);
        }

        public ActionResult Category(int id)
        {
            var category = db.Categories.Find(id);
            var subCategories = category.SubCategories;
            List<Category> categoryPath = GetCategoryPath(category);

            //AssignRandomImages(category.Products);
            ViewBag.Categories = subCategories;
            ViewBag.Categoryname = category.Name;
            ViewBag.CategoryPath = categoryPath;

            return View(category.Products);
        }

        private List<Category> GetCategoryPath(Category category)
        {
            List<Category> categoryPath = new List<Models.Entities.Category>();
            categoryPath.Add(category);
            while (category.ParentCategory != null)
            {
                categoryPath.Add(category.ParentCategory);
                category = category.ParentCategory;
            }
            categoryPath.Reverse();
            return categoryPath;
        }

        private void AssignRandomImages(IEnumerable<Product> products)
        {
            string imagesPath = "~/Content/images/portfolio/thumb/";
            var productImages = Directory.GetFiles(Server.MapPath(imagesPath));
            Random rng = new Random();
            foreach (var item in products)
            {
                item.MainImage = new ProductImage { Filename = imagesPath + Path.GetFileName(productImages[rng.Next(productImages.Length)]), CustomDirectory = true };
            }
        }

        public ActionResult Item(int id)
        {
            throw new NotImplementedException();
            Product p = db.Products.Find(id);
            if (p.MainImage == null)
            {
                p.MainImage = new ProductImage();
            }
            return View(p);
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Tender()
        {
            return View();
        }
    }
}