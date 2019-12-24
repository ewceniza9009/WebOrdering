using System;
using System.ComponentModel.DataAnnotations;

namespace AccountMateWebOrder.Models
{
    public class Inventory
    {

        public int Id { get; set; }
        public string ItemCode { get; set; }     
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string UOfM { get; set; }
        [Range(0, 100)]
        [Required(ErrorMessage = "Please input Quantity")]
        public int? Quantity { get; set; }
        public decimal ListPrice
        {
            get 
            {
                return Math.Round(_ListPrice ?? 0, 2);
            }
            set 
            {
                _ListPrice = value;
            }
        }
        private decimal? _ListPrice;
        public string ImagePath { get; set; }
        public string from { get; set; }
    }
}