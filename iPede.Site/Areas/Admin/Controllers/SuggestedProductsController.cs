using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iPede.Site.Models.Entities;

namespace iPede.Site.Areas.Admin.Controllers
{
    public class SuggestedProductsController : Controller
    {
        private iPedeContext db = new iPedeContext();

        // GET: Admin/SuggestedProducts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChildList()
        {
            var suggestedProducts = db.SuggestedProducts.ToList();
            return PartialView(suggestedProducts);
        }

        public ActionResult ChildAvailableList()
        {
            var products = AvailableProducts();

            return PartialView(products);
        }


        public ActionResult Add(int id)
        {
            Product p = db.Products.Find(id);
            if (db.SuggestedProducts.Count(s => s.ProductId == p.Id) == 0)
            {
                SuggestedProduct suggested = new SuggestedProduct()
                {
                    Product = p
                };
                db.SuggestedProducts.Add(suggested);
                db.SaveChanges();
                return PartialView("ChildList", db.SuggestedProducts);
            }

            return new HttpStatusCodeResult(400);
        }

        public ActionResult Remove(int id)
        {
            SuggestedProduct s = db.SuggestedProducts.Find(id);
            if (s != null)
            {

                db.SuggestedProducts.Remove(s);
                db.SaveChanges();
                return PartialView("ChildAvailableList", AvailableProducts());
            }

            return new HttpStatusCodeResult(400);
        }

        // GET: Admin/SuggestedProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuggestedProduct suggestedProduct = db.SuggestedProducts.Find(id);
            if (suggestedProduct == null)
            {
                return HttpNotFound();
            }
            return View(suggestedProduct);
        }

        // GET: Admin/SuggestedProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Admin/SuggestedProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,OrderingNumber")] SuggestedProduct suggestedProduct)
        {
            if (ModelState.IsValid)
            {
                db.SuggestedProducts.Add(suggestedProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", suggestedProduct.ProductId);
            return View(suggestedProduct);
        }

        // GET: Admin/SuggestedProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuggestedProduct suggestedProduct = db.SuggestedProducts.Find(id);
            if (suggestedProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", suggestedProduct.ProductId);
            return View(suggestedProduct);
        }

        // POST: Admin/SuggestedProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,OrderingNumber")] SuggestedProduct suggestedProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suggestedProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", suggestedProduct.ProductId);
            return View(suggestedProduct);
        }

        // GET: Admin/SuggestedProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuggestedProduct suggestedProduct = db.SuggestedProducts.Find(id);
            if (suggestedProduct == null)
            {
                return HttpNotFound();
            }
            return View(suggestedProduct);
        }

        // POST: Admin/SuggestedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuggestedProduct suggestedProduct = db.SuggestedProducts.Find(id);
            db.SuggestedProducts.Remove(suggestedProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private IQueryable<Product> AvailableProducts()
        {
            var suggested = db.SuggestedProducts.Select(s => s.Product);
            var products = db.Products.Where(p => !suggested.Contains(p));
            return products;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
