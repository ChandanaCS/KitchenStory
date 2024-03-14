using AutoMapper;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AdminService
    {
        private readonly IMapper _mapper;
        public AdminService()
        {
            AutoMapperConfig.Initialize();
            _mapper = AutoMapperConfig.Mapper;
        }
        public bool DeleteFoodItem(int foodItemId)
        {
            using(KitchenStoryDBEntities db = new KitchenStoryDBEntities())
            {
                FoodItem foodItem = db.FoodItems.Find(foodItemId);
                if(foodItem != null)
                {
                    db.FoodItems.Remove(foodItem);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool AddFoodItem(AddFoodItemModelDTO addFoodItemModelDTO)
        {
            using (KitchenStoryDBEntities db = new KitchenStoryDBEntities())
            {
                DbSet<Category> categories = db.Categories;
                Category category = categories.FirstOrDefault(c => c.CategoryName == addFoodItemModelDTO.CategoryName);
                if (category == null)
                {
                    return false;
                }
                FoodItem foodItem = new FoodItem();
                foodItem.Name = addFoodItemModelDTO.Name;
                foodItem.Description = addFoodItemModelDTO.Description;
                foodItem.Price = addFoodItemModelDTO.Price;
                foodItem.QuantityInStock = addFoodItemModelDTO.QuantityInStock;
                foodItem.ImageUrl = addFoodItemModelDTO.ImageUrl;
                foodItem.CategoryID = category.CategoryID;

                db.FoodItems.Add(foodItem);
                int result = db.SaveChanges();
                if(result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<CategoryDTO> GetAllCategory()
        {
            KitchenStoryDBEntities db = new KitchenStoryDBEntities();
            DbSet<Category> categories = db.Categories;
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return categoryDTOs;
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
        public bool ChangePassword(string emailID, string newPassword)
        {
            KitchenStoryDBEntities db = new KitchenStoryDBEntities();
            DbSet<User> users = db.Users;
            var passwordToUpdate = users.FirstOrDefault(u => u.Email == emailID);
            if(passwordToUpdate != null)
            {
                passwordToUpdate.Password = (newPassword);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
