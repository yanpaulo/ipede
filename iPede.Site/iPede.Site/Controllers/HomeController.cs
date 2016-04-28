using iPede.Site.Models;
using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace iPede.Site.Controllers
{
    public class HomeController : Controller
    {
        private iPedeContext db = new iPedeContext();

        public ActionResult Index()
        {
            var products = db.Products.Take(9).ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(bool? success)
        {
            if (success == true)
            {
                ViewBag.Message = "Mensagem enviada.";
                ViewBag.RedirectUrl = Url.Action("Contact");
            }
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

    }
}