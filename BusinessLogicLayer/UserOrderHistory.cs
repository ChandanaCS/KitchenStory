using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class UserOrderHistory
    {
        public int UserId { get; set; }
        public List<UserOrderHistoryItem> Orders { get; set; }
    }
    public class UserOrderHistoryItem
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderedItem> Items { get; set; }
    }

    public class OrderedItem
    {
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
