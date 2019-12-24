using System.Linq;
using System.Web.Mvc;

namespace AccountMateWebOrder.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.Title = "Login";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Customer customer)
        {

            if (ModelState.IsValid)
            {
                //TODO login authentication and process

                bool isAuth = false;

                using (var ctx = new Data.AMEntDataContext())
                {
                    isAuth = ctx.Customers.Where(x => x.CustomerCode.ToLower() == customer.CustomerCode.ToLower()).Any();

                    if (isAuth)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
            }

            return View(customer);
        }
    }
}