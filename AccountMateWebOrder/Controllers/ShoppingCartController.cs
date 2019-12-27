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

        public ActionResult EditProductLineItem(int id, int quantity) 
        {
            Services.ShoppingCartService.EditItemOnShoppingCart(id, quantity);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveProduct(int id) 
        {
            Services.ShoppingCartService.DeleteItemOnShoppingCart(id);

            return RedirectToAction("Index");
        }

        public ActionResult SaveCart() {

            return View();
        }
    }
}