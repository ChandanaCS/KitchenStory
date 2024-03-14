using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<FoodItemDTO, FoodItem>();
                cfg.CreateMap<FoodItem, FoodItemDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Category, CategoryDTO>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
