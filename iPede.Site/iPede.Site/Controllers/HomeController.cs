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
            //var bannerItems = db.BannerItems.ToList();
            //var categories = db.Categories.Where(item => item.ParentCategoryId == null);
            //var products = db.Products.Take(9).ToList();
            //ViewBag.BannerItems = bannerItems;
            //ViewBag.Categories = categories;
            //HomePageViewModel model = new HomePageViewModel() { BannerItems = bannerItems, Categories = categories, Products = products };
            //return View(model);
            return Redirect("~/Admin");
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

        [HttpPost]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (StreamReader templateReader = new StreamReader(Server.MapPath("~/Content/mail-templates/contact.txt")))
                {
                    string mailText = templateReader.ReadToEnd();
                    mailText = mailText
                        .Replace("<#Name#>", model.Name)
                        .Replace("<#Email#>", model.Email)
                        .Replace("<#Phone#>", model.Phone)
                        .Replace("<#Content#>", model.Content);

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("handicap@yanscorp.com", model.Name);

                    message.Subject = "Handicap(site) - Contato";
                    message.To.Add("faleconoscooldiscool@gmail.com");
                    message.To.Add("handicap@yanscorp.com");
                    message.Bcc.Add("yanpaulo@hotmail.com");
                    message.Body = mailText;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Send(message);
                }
                return RedirectToAction("Contact", new { success = true });
            }
            return View(model);
        }

        public ActionResult Service()
        {
            var services = db.Services.ToList();
            return View(services);
        }

        public ActionResult Products()
        {
            return View();
        }

    }
}