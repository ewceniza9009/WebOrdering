using System;
using System.Web.Mvc;

namespace AccountMateWebOrder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Models.Page.Pager pagedModel)
        {
            ViewBag.Search = Request.QueryString["Search"] ?? "".ToString();

            var allActiveInventories = Services.InventoryService.GetAllActiveInventories(ViewBag.search, pagedModel);
            var inventoryCount = Services.InventoryService.AllActiveInventoriesCount;

            pagedModel.PageCount = Math.Ceiling(inventoryCount / (double)pagedModel.ItemsPerPage);

            if (pagedModel.Button == "next")
            {
                if (Models.Page.PageNumber.Instance.InventoryCurrentPage <= (pagedModel.PageCount / pagedModel.PageRange))
                {
                    Models.Page.PageNumber.Instance.InventoryCurrentPage += 1;
                }
            }
            else if (pagedModel.Button == "prev")
            {
                if (Models.Page.PageNumber.Instance.InventoryCurrentPage > 1)
                {
                    Models.Page.PageNumber.Instance.InventoryCurrentPage -= 1;
                }
            }

            pagedModel.EndPage = Models.Page.PageNumber.Instance.InventoryCurrentPage * pagedModel.PageRange;

            if (pagedModel.PageCount < pagedModel.EndPage && pagedModel.PageCount != 1) pagedModel.EndPage = (int)pagedModel.PageCount;
            if (pagedModel.PageCount < pagedModel.PageRange) pagedModel.EndPage = (int)pagedModel.PageCount;

            pagedModel.StartPage = (pagedModel.EndPage - pagedModel.PageRange) + 1;

            if (pagedModel.StartPage <= 0) pagedModel.StartPage = 1;

            ViewBag.Title = "Products";
            ViewBag.AllActiveInventories = allActiveInventories;

            return View(pagedModel);
        }
        public ActionResult Detail(Models.Inventory model)
        {
            if (model.from != "shoppingcart")
            {
                model.Quantity = model.Quantity ?? 1;

                foreach (var key in ModelState.Keys)
                {
                    ModelState[key].Errors.Clear();
                }
            }
            else
            {
                model.Quantity = model.Quantity ?? null;
            }

            ViewBag.Title = "Detail";
            ViewBag.Inventory = Services.InventoryService.GetInventory(model.Id, model.Quantity);

            return View(ViewBag.Inventory as Models.Inventory);
        }
    }
}