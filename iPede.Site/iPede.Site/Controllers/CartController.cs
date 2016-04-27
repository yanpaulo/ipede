using iPede.Site.Models;
using iPede.Site.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

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

                Order order = new Order { OrderStatusId = int.Parse(ConfigurationManager.AppSettings["DefaultOrderStatusId"]) };

                cart.Items.ForEach(cartItem => {
                    order.Items.Add(
                        new OrderItem {
                        ProductId = cartItem.Product.ProductId,
                        Quantity = cartItem.Ammount,
                        Price = cartItem.Ammount * cartItem.Product.Price } );
                });
                db.Orders.Add(order);
                db.SaveChanges();

                CleanCart();
                return RedirectToAction("Finish");
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