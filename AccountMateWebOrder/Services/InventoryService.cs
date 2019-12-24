using System.Collections.Generic;
using System.Linq;

namespace AccountMateWebOrder.Services
{
    public static class InventoryService
    {
        private static int _AllActiveInventoriesCount;
        public static int AllActiveInventoriesCount 
        {
            get
            {
                return _AllActiveInventoriesCount;
            }
        }
        public static List<Models.Inventory> GetAllActiveInventories(string search, Models.Page.Pager pagedModel)
        {
            var result = new List<Models.Inventory>();

            using (var ctx = new Data.AMEntDataContext())
            {
                var data = ctx.Inventories
                        .Where(x => x.Status == 1
                            && x.InventoryWarehouses.Sum(y => y.OnhandQty) > 0);               

                _AllActiveInventoriesCount = data.Count();
                
                if (string.IsNullOrWhiteSpace(search))
                {
                    result = data
                        .Select(x => new Models.Inventory
                        {
                            Id = x.ID,
                            ItemCode = x.ItemCode,
                            Description = x.Description,
                            ListPrice = x.InventoryPrices != null ?
                                x.InventoryPrices.FirstOrDefault() != null ?
                                    x.InventoryPrices.FirstOrDefault().InventoryListPrices != null && x.InventoryPrices.FirstOrDefault().InventoryListPrices.Count() > 0 ?
                                        x.InventoryPrices.FirstOrDefault().InventoryListPrices.Max(a => a.ListPrice) : 0
                                    : 0
                                : 0,
                            ImagePath = Resource.StaticResource.ImagePath
                        })
                        .Skip(pagedModel.Start)
                        .Take(pagedModel.ItemsPerPage)
                        .ToList();
                }
                else
                {
                    var newData = data
                        .Where(x => x.ItemCode.ToLower().Contains(search.ToLower()));

                    _AllActiveInventoriesCount = newData.Count();

                    result = newData
                        .Select(x => new Models.Inventory
                        {
                            Id = x.ID,
                            ItemCode = x.ItemCode,
                            Description = x.Description,
                            ListPrice = x.InventoryPrices != null ?
                                x.InventoryPrices.FirstOrDefault() != null ?
                                    x.InventoryPrices.FirstOrDefault().InventoryListPrices != null && x.InventoryPrices.FirstOrDefault().InventoryListPrices.Count() > 0 ?
                                        x.InventoryPrices.FirstOrDefault().InventoryListPrices.Max(a => a.ListPrice) : 0
                                    : 0
                                : 0,
                            ImagePath = Resource.StaticResource.ImagePath
                        })
                        .Skip(pagedModel.Start)
                        .Take(pagedModel.ItemsPerPage)
                        .ToList();
                }
            }

            return result;
        }
        public static Models.Inventory GetInventory(int id, int? qty)
        {
            var result = new Models.Inventory();

            using (var ctx = new Data.AMEntDataContext())
            {
                result = ctx.Inventories
                    .Where(x => x.ID == id)
                    .Select(x => new Models.Inventory
                    {
                        Id = x.ID,
                        ItemCode = x.ItemCode,
                        Description = x.Remarks.Trim().Length > 0 ? x.Remarks : x.Description,
                        ShortDescription = x.ShortDescription,
                        UOfM = x.UnitOfMeasure.UOMCode,
                        Quantity = qty,
                        ListPrice = x.InventoryPrices != null ?
                            x.InventoryPrices.FirstOrDefault() != null ?
                                x.InventoryPrices.FirstOrDefault().InventoryListPrices != null && x.InventoryPrices.FirstOrDefault().InventoryListPrices.Count() > 0 ?
                                    x.InventoryPrices.FirstOrDefault().InventoryListPrices.Max(a => a.ListPrice) : 0
                                : 0
                            : 0,
                        ImagePath = Resource.StaticResource.ImagePath
                    }).SingleOrDefault();
            }

            return result;
        }
    }
}