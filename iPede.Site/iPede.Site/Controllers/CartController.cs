using iPede.Site.Models;
using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Uol.PagSeguro.Domain;

namespace iPede.Site.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "783338a6-591b-4544-b3c7-f9889715f73d";

        private iPedeContext db = new iPedeContext();

        public ActionResult Icon()
        {
            return PartialView(GetCart());
        }

        // GET: Cart
        public ActionResult Index()
        {
            ShoppingCartViewModel model = GetCart();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ShoppingCartViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Finish");
            }
            return View(model);
        }

        public ActionResult ItemAddedPartial(int id)
        {
            var item = db.Products.Find(id);
            return PartialView(item);
        }

        [ActionName("Finish")]
        public ActionResult FinishConfirmed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Finish()
        {
            ShoppingCartViewModel cart = GetCart();
            if (ModelState.IsValid)
            {
                PaymentRequest payment = new PaymentRequest();

                foreach (var cartItem in cart.Items)
                {
                    payment.Items.Add(new Item(
                        cartItem.Product.ProductId.ToString(),
                        cartItem.Product.Name,
                        cartItem.Ammount,
                        cartItem.Product.Price));
                }

                CleanCart();
                payment.RedirectUri = Request.Url;
                AccountCredentials credentials = new AccountCredentials(
                    "yanpaulo@hotmail.com",
                    "F93A9C35B6D046FAAC451A92A0C38CD3");
                return Redirect(payment.Register(credentials).ToString());
            }

            return View("Index", cart);
        }
        

        [HttpPost]
        public ActionResult Add(int id)
        {
            ShoppingCartViewModel cart = GetCart();
            Product product = db.Products.Find(id);

            if (cart.Items.Count(item => item.Product.ProductId == id) == 0)
            {
                cart.Items.Add(new ShoppingCartViewModelItem() { Product = product, Ammount = 1 });
            }

            return GetCartJson(cart);
        }

        public ActionResult Remove(int index)
        {
            ShoppingCartViewModel cart = GetCart();
            cart.Items.RemoveAt(index);
            return PartialView("ListPartial", cart);
        }

        public ActionResult UpdateQuantity(int index, int quantity)
        {
            ShoppingCartViewModel cart = GetCart();
            cart.Items[index].Ammount = quantity;
            return PartialView("ListPartial", cart);
        }

        [HttpPost]
        public ActionResult GetData()
        {
            ShoppingCartViewModel cart = GetCart();
            return GetCartJson(cart);
        }

        private ActionResult GetCartJson(ShoppingCartViewModel cart)
        {
            return Json(new { ProductIds = cart.Items.Select(item => item.Product.ProductId) });
        }

        private ShoppingCartViewModel GetCart()
        {
            ShoppingCartViewModel cart = (ShoppingCartViewModel)Session[CartSessionKey];
            if (cart == null)
            {
                cart = new ShoppingCartViewModel();
                Session[CartSessionKey] = cart;
            }
            return cart;
        }

        private void CleanCart()
        {
            Session[CartSessionKey] = new ShoppingCartViewModel();
        }
    }
}