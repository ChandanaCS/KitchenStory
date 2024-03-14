using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer;
using WebAPI.Models;

namespace WebAPI
{
    public class AutoMapperConfigAPI
    {
        public static IMapper Mapper { get; private set; }
        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, UserModel>();
                cfg.CreateMap<UserModel, UserDTO>();
                cfg.CreateMap<FoodItemDTO, FoodItemModel>();
                cfg.CreateMap<FoodItemModel, FoodItemDTO>();
                cfg.CreateMap<UserOrderHistory, UserOrderHistoryModel>();
                cfg.CreateMap<UserOrderHistoryItem, UserOrderHistoryItemModel>();
                cfg.CreateMap<OrderedItem, OrderedItemModel>();
                cfg.CreateMap<AddFoodItemModelDTO, AddFoodItemModel>();
                cfg.CreateMap<AddFoodItemModel, AddFoodItemModelDTO>();
                cfg.CreateMap<CategoryDTO, CategoryModel>();
                cfg.CreateMap<CategoryModel, CategoryDTO>();
            });
            Mapper = config.CreateMapper();
        }
    }
}