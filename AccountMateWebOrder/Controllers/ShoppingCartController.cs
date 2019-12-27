using System;
using System.Web.Mvc;

namespace AccountMateWebOrder.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            ViewBag.Title = "Cart";

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Models.Inventory model) 
        {
            if (ModelState.IsValid) 
            {
                Services.ShoppingCartService.AddItemToShoppingCart(model);
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Detail", "Home", new
            {
                id = model.Id,
                quantity = model.Quantity,
                from = "shoppingcart"
            });
        }

        public JsonResult EditProductLineItem(int id, int quantity) 
        {
            Services.ShoppingCartService.EditItemOnShoppingCart(id, quantity);
            var result = Services.ShoppingCartService.Cart.Find(x => x.CartId == id);

            var sumOfTotalPrice = 0m;

            foreach (var x in Services.ShoppingCartService.Cart) 
            {
                sumOfTotalPrice += x.TotalPrice;
            }

            return Json(new { 
                TotalPrice = result.TotalPrice,
                SumOfTotalPrice = sumOfTotalPrice
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveProduct(int id) 
        {
            Services.ShoppingCartService.DeleteItemOnShoppingCart(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveCart() {
            return RedirectToAction("Index");
        }
    }
}