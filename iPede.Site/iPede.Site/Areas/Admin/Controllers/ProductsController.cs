using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iPede.Site.Models.Entities;
using iPede.Site.Models;
using System.IO;
using ImageResizer;
using System.Diagnostics;

namespace iPede.Site.Areas.Admin.Controllers
{
    public class ProductSessionObject
    {
        public ProductSessionObject()
        {
            Images = new List<ProductImage>();
        }
        public List<ProductImage> Images { get; set; }
    }

    public class ProductsController : Controller
    {
        private iPedeContext db = new iPedeContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public string GenerateSessionKey()
        {
            string key = Guid.NewGuid().ToString();
            while (Session[key] != null)
            {
                key = Guid.NewGuid().ToString();
            }
            return key;
        }

        public ActionResult ImagesUpload(HttpPostedFileBase[] files, string sessionKey, int? defaultImageIndex)
        {
            ProductSessionObject sObject = (ProductSessionObject)Session[sessionKey];
            string thumbsPath = ProductImage.DefaultThumbDirectory,
                imagesPath = ProductImage.DefaultImageDirectory;
            //If images were uploaded and there were no default image selected,
            //then the first one is our default.
            if (files.Count() > 0 && !defaultImageIndex.HasValue)
            {
                defaultImageIndex = 0;
            }
            //Defines the image resizing and format settings for thumbnails
            Instructions thumbResizeSettings = new Instructions() { Width = 300, Height = 400, Mode = FitMode.Pad };

            foreach (var item in files)
            {
                string extension = Path.GetExtension(item.FileName).ToLower();
                if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                {
                    ProductImage image = new ProductImage();
                    //First we resize/save the thmbnail.
                    //ImageResizer can generate the filename using variables like these <guid> and <ext> below.
                    //I guess their working is obvious, so I won't explain.
                    ImageResizer.ImageJob thumbImageJob = new ImageResizer.ImageJob(item,
                        string.Format("{0}<guid>.<ext>", thumbsPath),
                        thumbResizeSettings);
                    thumbImageJob.Build();

                    //Setting the filename for the full image and for its thumbnail
                    image.Filename = Path.GetFileName(thumbImageJob.FinalPath);
                    image.ThumbFilename = image.Filename;

                    //Finally, save the full image with the same name as the thumbnail
                    item.SaveAs(Server.MapPath(imagesPath + image.Filename));

                    //Adds the newly uploaded image object do the Session Object. NOT to database.
                    sObject.Images.Add(image);
                }
            }

            ViewBag.defaultImageIndex = defaultImageIndex;

            return PartialView(sObject.Images);
        }

        public ActionResult DeleteImage(int index, string sessionKey, int? defaultImageIndex, bool deleteFromDisk)
        {
            ProductSessionObject sObject = (ProductSessionObject)Session[sessionKey];

            ProductImage image = sObject.Images[index];
            string localPath = Server.MapPath(image.Url);

            sObject.Images.Remove(image);
            if (deleteFromDisk)
            {
                System.IO.File.Delete(localPath);
            }

            if (sObject.Images.Count > 0 && defaultImageIndex.HasValue)
            {
                if (defaultImageIndex > 0 && index <= defaultImageIndex)
                {
                    defaultImageIndex--;
                }
            }
            else
            {
                defaultImageIndex = null;
            }
            ViewBag.defaultImageIndex = defaultImageIndex;

            return PartialView("ImagesUpload", sObject.Images);

        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "FullName");
            string key = GenerateSessionKey();
            Session[key] = new ProductSessionObject();
            ViewBag.sessionKey = key;
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,CategoryID,Name,ShortDescription,FullDescription,Price,DefaultImageIndex")] ProductViewModel model, string sessionKey)
        {
            ProductSessionObject sObject = (ProductSessionObject)Session[sessionKey];
            model.Images = sObject.Images;
            model.Product.Images = sObject.Images;
            if (ModelState.IsValid)
            {
                db.Products.Add(model.Product);
                db.SaveChanges();
                if (model.DefaultImageIndex.HasValue)
                {
                    model.Product.MainImage = sObject.Images[model.DefaultImageIndex.Value];
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.defaultImageIndex = model.DefaultImageIndex;
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "FullName", model.CategoryId);

            return View(model);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductViewModel viewModel = new ProductViewModel(product);
            ProductSessionObject sObject = new ProductSessionObject();
            string key = GenerateSessionKey();
            int defaultImageIndex = 0;

            viewModel.Images = product.Images.ToList();

            for (int i = 0; i < viewModel.Images.Count; i++)
            {
                if (viewModel.Product.MainImage == viewModel.Images[i])
                {
                    defaultImageIndex = i;
                }
            }

            sObject.Images = viewModel.Images;
            Session[key] = sObject;
            ViewBag.DefaultImageIndex = defaultImageIndex;
            ViewBag.sessionKey = key;
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "FullName", product.CategoryId);
            return View(viewModel);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,CategoryID,Name,ShortDescription,FullDescription,Price,DefaultImageIndex")] ProductViewModel model, string sessionKey)
        {
            ProductSessionObject sObject = (ProductSessionObject)Session[sessionKey];
            model.Images = sObject.Images;
            if (ModelState.IsValid)
            {
                //Anexando o objeto ao DataContext, o objeto no DataContext será "preenchido" com as novas informações
                db.Entry(model.Product).State = EntityState.Modified;
                //O objeto obtido via POST não contém as informações de imagem, mas o do DataContext sim.
                //Ao recuperarmos ele, ele terá as informações atualizadas (vide linha anterior) e também as imagens.
                model.Product = db.Products.Include(path => path.Images).Single(item => item.ProductId == model.ProductId);

                //Buscando os itens que estão no Model atual, mas não no banco de dados
                var removedImages = model.Product.Images.Where(dbItem =>
                    sObject.Images.Count(sessionItem =>
                        sessionItem.ProductImageId == dbItem.ProductImageId) == 0).ToList();

                foreach (var image in removedImages)
                {
                    db.ProductImages.Remove(image);

                    //Tentando excluir as imagens do disco
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(image.Url));
                    }
                    catch (IOException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    try
                    {
                        System.IO.File.Delete(Server.MapPath(image.ThumbUrl));
                    }
                    catch (IOException ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

                //Inclui as imagens novas do Model
                foreach (var item in sObject.Images.Where(item => item.ProductImageId == 0))
                {
                    model.Product.Images.Add(item);
                }

                //De a imagem padrão estiver selecionada e estiver na faixa de imagens existentes no array mo Model,
                //seta o ID da imagem principal do produto para DefaultImageIndex.
                if (model.DefaultImageIndex.HasValue && model.DefaultImageIndex.Value >= 0 && model.DefaultImageIndex.Value < model.Images.Count)
                {
                    ProductImage defaultImage = model.Images[model.DefaultImageIndex.Value];
                    //Se for uma imagem já presente no DB
                    if (defaultImage.ProductImageId != 0)
                    {
                        model.MainImage = null;
                        model.Product.MainImageId = model.Images[model.DefaultImageIndex.Value].ProductImageId;
                    }
                    //Se não, se for recém postada
                    else
                    {
                        model.Product.MainImageId = null;
                        model.Product.MainImage = defaultImage;
                    }
                   
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "FullName", model.CategoryId);
            return View(model);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            //We need to remove the images first
            //Create a copy of the EntitySet as a List, because they will be deleted and it can crash the navigation.
            foreach (ProductImage image in product.Images.ToList())
            {
                //These are not critical operations, and no one cares about them. So they go in a try/catch block.
                try
                {
                    System.IO.File.Delete(Server.MapPath(image.Url));
                    System.IO.File.Delete(Server.MapPath(image.ThumbUrl));
                }
                catch (Exception)
                {

                }
                db.ProductImages.Remove(image);
            }
            db.SaveChanges();
            db.Products.Remove(product);
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
    }
}
