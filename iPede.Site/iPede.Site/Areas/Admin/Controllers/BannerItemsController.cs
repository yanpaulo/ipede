using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iPede.Site.Models.Entities;
using System.IO;

namespace iPede.Site.Areas.Admin.Controllers
{
    public class BannerItemsController : Controller
    {
        private iPedeContext db = new iPedeContext();

        // GET: Admin/BannerItems
        public ActionResult Index()
        {
            return View(db.BannerItems.ToList());
        }

        // GET: Admin/BannerItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannerItem bannerItem = db.BannerItems.Find(id);
            if (bannerItem == null)
            {
                return HttpNotFound();
            }
            return View(bannerItem);
        }

        // GET: Admin/BannerItems/Create
        public ActionResult Create()
        {
            string localPrefix = GetLocalPrefix();
            ViewBag.LocalPrefix = localPrefix;
            return View();
        }

        private string[] validImageExtensions = { ".jpg", ".png", ".bmp" };
        private string imageDirectory = "~/Content/banner-images/";
        
        // POST: Admin/BannerItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BannerItemId,Title,Text,ImageUrl,VerticalPosition,LinkUrl,LinkUrlType,ImageFile")] BannerItem bannerItem, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                string extension = Path.GetExtension(ImageFile.FileName).ToLower();
                if (validImageExtensions.Contains(extension))
                {
                    string fullImageDirectory = Request.MapPath(imageDirectory);
                    var existingFiles = Directory.GetFiles(Request.MapPath(imageDirectory)).Select(item => item.ToLower());
                    string filename;
                    do
                    {
                        filename = Guid.NewGuid().ToString().ToLower() + extension;
                    } while (existingFiles.Contains(filename));

                    ImageFile.SaveAs(fullImageDirectory + filename);
                    bannerItem.ImageUrl = imageDirectory + filename;

                    ModelState["ImageUrl"].Errors.Clear();
                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "Tipo de arquivo inválido.");
                }
                
            }

            if (ModelState.IsValid)
            {
                db.BannerItems.Add(bannerItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bannerItem);
        }

        // GET: Admin/BannerItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannerItem bannerItem = db.BannerItems.Find(id);
            if (bannerItem == null)
            {
                return HttpNotFound();
            }
            string localPrefix = GetLocalPrefix();
            ViewBag.LocalPrefix = localPrefix;

            return View(bannerItem);
        }

        // POST: Admin/BannerItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BannerItemId,Title,Text,ImageUrl,VerticalPosition,LinkUrl,LinkUrlType,ImageFile")] BannerItem bannerItem, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                string extension = Path.GetExtension(ImageFile.FileName).ToLower();
                if (validImageExtensions.Contains(extension))
                {
                    string fullImageDirectory = Request.MapPath(imageDirectory);
                    var existingFiles = Directory.GetFiles(Request.MapPath(imageDirectory)).Select(item => item.ToLower());
                    
                    string filename;
                    do
                    {
                        filename = Guid.NewGuid().ToString().ToLower() + extension;
                    } while (existingFiles.Contains(filename));

                    
                    ImageFile.SaveAs(fullImageDirectory + filename);
                    bannerItem.ImageUrl = imageDirectory + filename;

                    ModelState["ImageUrl"].Errors.Clear();
                    
                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "Tipo de arquivo inválido.");
                }

            }

            if (ModelState.IsValid)
            {
                System.Data.Entity.Infrastructure.DbEntityEntry entry = db.Entry(bannerItem);
                entry.State = EntityState.Modified;
                BannerItem previousBannerState = (BannerItem)entry.GetDatabaseValues().ToObject();
                string previousFileName = previousBannerState.ImageUrl;
                //string previousFilename = (string)entry.OriginalValues["ImageUrl"];

                if (!string.IsNullOrEmpty(previousFileName) && previousFileName != bannerItem.ImageUrl)
                {
                    previousFileName = Request.MapPath(previousFileName);
                    try
                    {
                        System.IO.File.Delete(previousFileName);
                    }
                    catch
                    {

                    }
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bannerItem);
        }

        // GET: Admin/BannerItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BannerItem bannerItem = db.BannerItems.Find(id);
            if (bannerItem == null)
            {
                return HttpNotFound();
            }
            return View(bannerItem);
        }

        // POST: Admin/BannerItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BannerItem bannerItem = db.BannerItems.Find(id);
            try
            {
                string fileName = Request.MapPath(bannerItem.ImageUrl);
                System.IO.File.Delete(fileName);
            }
            catch
            {
            }
            db.BannerItems.Remove(bannerItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetLocalPrefix()
        {
            string appPath = Request.ApplicationPath != "/" ? Request.ApplicationPath : string.Empty,
                localPrefix = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, appPath).ToLower();
            return localPrefix;
        }

    }
}
