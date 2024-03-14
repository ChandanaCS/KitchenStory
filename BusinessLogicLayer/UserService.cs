using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class UserService
    {
        private readonly IMapper _mapper;
        public UserService()
        {
            AutoMapperConfig.Initialize();
            _mapper = AutoMapperConfig.Mapper;
        }
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                byte[] truncatedBytes = new byte[10];
                Array.Copy(bytes, truncatedBytes, 10);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < truncatedBytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public UserDTO AuthenticateUser(string email,string password)
        {
            KitchenStoryDBEntities db = new KitchenStoryDBEntities();
            DbSet<User> userDb = db.Users;
            var authenticatedUser = userDb.FirstOrDefault(u => u.Email == email && u.Password == (password));
            if(authenticatedUser != null)
            {
                UserDTO userDTO = _mapper.Map<UserDTO>(authenticatedUser);
                return userDTO;
            }
            return null;
        }
        public List<FoodItemDTO> GetAllFoodItems()
        {
            KitchenStoryDBEntities db = new KitchenStoryDBEntities();
            DbSet<FoodItem> foodItems = db.FoodItems;
            List<FoodItemDTO> foodItemDTOs = _mapper.Map<List<FoodItemDTO>>(foodItems);
            return foodItemDTOs;
        }
        public UserOrderHistory GetOrderHistory(int userId)
        {
            KitchenStoryDBEntities db = new KitchenStoryDBEntities();
            var orderHistory = db.Orders
            .Where(o => o.UserID == userId)
            .OrderByDescending(o => o.OrderDate) 
            .Select(order => new UserOrderHistoryItem
            {
                OrderId = order.OrderID,
                OrderDate = order.OrderDate.GetValueOrDefault(),
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(item => new OrderedItem
                {
                    FoodItemId = item.FoodItemID.GetValueOrDefault(),
                    Quantity = item.Quantity.GetValueOrDefault(),
                    ItemPrice = item.ItemPrice
                }).ToList()
            })
            .ToList();

            return new UserOrderHistory
            {
                UserId = userId,
                Orders = orderHistory
            };

        }
        public List<FoodItemDTO> GetSearchResults(string search)
        {
            KitchenStoryDBEntities db = new KitchenStoryDBEntities();
            DbSet<FoodItem> foodItems = db.FoodItems;
            DbSet<Category> categories = db.Categories;
            List<FoodItemDTO> foodItemDTOs = new List<FoodItemDTO>();
            var query = from foodItem in db.FoodItems
                        join category in db.Categories on foodItem.CategoryID equals category.CategoryID
                        where foodItem.Name.Contains(search) || category.CategoryName.Contains(search)
                        select new FoodItemDTO
                        {
                            FoodItemID = foodItem.FoodItemID,
                            Name = foodItem.Name,
                            Description = foodItem.Description,
                            Price = foodItem.Price,
                            QuantityInStock = foodItem.QuantityInStock,
                            CategoryID = foodItem.CategoryID,
                            ImageUrl = foodItem.ImageUrl,
                        };
            foodItemDTOs = query.ToList();
            return foodItemDTOs;
        }
    }
}
