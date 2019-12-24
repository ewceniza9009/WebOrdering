using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountMateWebOrder.Services
{
    public static class ShoppingCartService
    {
        public static List<Models.ShoppingCart> Cart 
        {
            get 
            {
                if (_Cart == null)
                {
                    _Cart = GetShoppingCart(); ;
                }

                return _Cart;
            }
            set 
            {
                _Cart = value;
            } 
        }

        private static List<Models.ShoppingCart> _Cart;

        private static List<Models.ShoppingCart> GetShoppingCart()
        {
            int ctr = 0;

            var shoppingCart = new List<Models.ShoppingCart>();

            using (var ctx = new Data.AMEntDataContext())
            {
                foreach (var x in ctx.Inventories)
                {
                    if (ctr < 3)
                    {
                        var qty = 1; //new Random().Next(1, 5);
                        var lstPrice = x.InventoryPrices != null ?
                                x.InventoryPrices.FirstOrDefault() != null ?
                                    x.InventoryPrices.FirstOrDefault().InventoryListPrices != null && x.InventoryPrices.FirstOrDefault().InventoryListPrices.Count() > 0 ?
                                        x.InventoryPrices.FirstOrDefault().InventoryListPrices.Max(a => a.ListPrice) : 0
                                    : 0
                                : 0;
                        var totalPrice = qty * lstPrice;

                        shoppingCart.Add(new Models.ShoppingCart()
                        {
                            CartId = x.ID,
                            ProductCode = x.ItemCode,
                            ProductName = x.Description,
                            Size = Resource.StaticResource.Size[new Random().Next(0, 2)],
                            Color = Resource.StaticResource.Color[new Random().Next(0, 4)],
                            Quantity = qty,
                            CurrencyName = "USD",
                            ListPrice = lstPrice,
                            TotalPrice = totalPrice,
                            ImagePath = Resource.StaticResource.ImagePath

                        });
                    }
                    else
                    {
                        break;
                    }

                    ctr++;
                }
            }

            return shoppingCart;
        }
        public static void AddItemToShoppingCart(Models.Inventory model) 
        {
            using (var ctx = new Data.AMEntDataContext())
            {
                var prod = ctx.Inventories.Where(x => x.ID == model.Id).SingleOrDefault();

                //var qty = new Random().Next(1, 5);
                int? qty = model.Quantity;
                var lstPrice = prod.InventoryPrices != null ?
                        prod.InventoryPrices.FirstOrDefault() != null ?
                            prod.InventoryPrices.FirstOrDefault().InventoryListPrices != null && prod.InventoryPrices.FirstOrDefault().InventoryListPrices.Count() > 0 ?
                                prod.InventoryPrices.FirstOrDefault().InventoryListPrices.Max(a => a.ListPrice) : 0
                            : 0
                        : 0;
                var totalPrice = (qty??0) * lstPrice;

                Cart.Add(new Models.ShoppingCart()
                {
                    CartId = prod.ID,
                    ProductCode = prod.ItemCode,
                    ProductName = prod.Description,
                    Size = Resource.StaticResource.Size[new Random().Next(0, 2)],
                    Color = Resource.StaticResource.Color[new Random().Next(0, 4)],
                    Quantity = qty??0,
                    CurrencyName = "USD",
                    ListPrice = lstPrice,
                    TotalPrice = totalPrice,
                    ImagePath = Resource.StaticResource.ImagePath

                });
            }
        }
        public static void EditItemOnShoppingCart(int id, int quantity)
        {
            var prod = Cart.Where(x => x.CartId == id).FirstOrDefault();

            prod.Quantity = quantity;
            prod.TotalPrice = prod.Quantity * prod.ListPrice;
        }
        public static void DeleteItemOnShoppingCart(int id) 
        {
            var prod = Services.ShoppingCartService.Cart.Where(x => x.CartId == id).FirstOrDefault();

            Cart.Remove(prod);
        }
    }
}