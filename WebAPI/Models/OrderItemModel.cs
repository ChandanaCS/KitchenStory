using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class OrderItemModel
    {
        public int OrderItemID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> FoodItemID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal ItemPrice { get; set; }

    }
}
