using System;

namespace AccountMateWebOrder.Models
{
    public class ShoppingCart
    {
        public int CartId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public decimal ListPrice 
        {
            get 
            {
                return _ListPrice ?? 0;
            }
            set 
            {
                _ListPrice = value;
            }
        }
        private decimal? _ListPrice;
        public decimal TotalPrice 
        {
            get 
            {
                return Math.Round(_TotalPrice ?? 0, 2);
            }
            set 
            {
                _TotalPrice = value;
            } 
        }
        private decimal? _TotalPrice { get; set; }
        public string CurrencyName { get; set; }
        public string ImagePath { get; set; }
    }
}