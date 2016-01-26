using iPede.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iPede.Site.Controllers
{
    public class MenuController : Controller
    {
        private MenuModel GetMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            items.Add(new MenuItem { Text = "Loja", Controller = "Products", Action = "Index" });
            items.Add(new MenuItem { Text = "Equipe", Controller = "Home", Action = "About" });
            items.Add(new MenuItem { Text = "Contato", Controller = "Home", Action = "Contact" });
            items.Add(new MenuItem { Text = "Blog", Url = "javascript:void()" });

            foreach (var item in items)
            {
                if (item.Url == null)
                {
                    item.Url = Url.Action(item.Action, item.Controller);
                    item.Active =
                    ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString().ToLower() == item.Action.ToLower() &&
                    ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString().ToLower() == item.Controller.ToLower();
                }
                else
                {
                    item.CustomUrl = true;
                }
            }
            return new MenuModel(items.AsEnumerable());
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView(GetMenu());
        }

        

        
    }
}