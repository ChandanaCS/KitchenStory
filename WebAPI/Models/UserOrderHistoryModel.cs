using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class UserOrderHistoryModel
    {
        public int UserId { get; set; }
        public List<UserOrderHistoryItemModel> Orders { get; set; }
    }
    public class UserOrderHistoryItemModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderedItemModel> Items { get; set; }
    }

    public class OrderedItemModel
    {
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
    }
}