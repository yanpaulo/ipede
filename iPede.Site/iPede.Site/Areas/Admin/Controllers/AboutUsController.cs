using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace iPede.Site.Areas.Admin.Controllers
{
    public class AboutUsController : Controller
    {
        /// <summary>
        /// The first area of the page is 1. Used for Edition.
        /// </summary>
        public const int MIN_QUADRANT = 1;
        /// <summary>
        /// The total number of areas of the page. Used for Edition.
        /// </summary>
        public const int MAX_QUADRANT = 4;

        private iPedeContext db = new iPedeContext();

        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            /* There is just one tuple for the page at the database. Return it. */
            return View(db.AboutUs.FirstOrDefault());
        }

        // GET: Admin/AboutUs/Edit/1
        public ActionResult Edit(int? id)
        {
            // If id is not in [1, 4], there is no such an area to edit.
            if (id > MAX_QUADRANT || id < MIN_QUADRANT)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            AboutUs aboutUs = db.AboutUs.FirstOrDefault();
            if (aboutUs == null)
            {
                aboutUs = new AboutUs();
                db.AboutUs.Add(aboutUs);
                db.SaveChanges();
            }

            ViewBag.Quadrant = id;

            return View(aboutUs);
        }

        // POST: Admin/AboutUs/Edit/1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AboutUs aboutUs)
        {
            // There is no validation because default values were already set on model.

            db.Entry(aboutUs).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}