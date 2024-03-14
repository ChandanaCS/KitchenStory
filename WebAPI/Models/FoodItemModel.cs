using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer;

namespace WebAPI.Models
{
    public class FoodItemModel
    {
        public int FoodItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryID { get; set; }
        public string ImageUrl { get; set; }
    }
}