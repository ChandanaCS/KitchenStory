using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLogicLayer;
using System.Web.Http.Cors;
using WebAPI.Models;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IMapper _mapper;
        public UserController()
        {
            AutoMapperConfigAPI.Initialize();
            _mapper = AutoMapperConfigAPI.Mapper;
        }
        
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [HttpPost]
        [Route("api/user/registereduserlogin")]
        public IHttpActionResult PostRegisterUserLogin([FromBody]JObject data)
        {
            var email = data["email"].ToString();
            var password = data["password"].ToString();
            UserService userService = new UserService();
            UserDTO userOrAdmin = userService.AuthenticateUser(email, password);
            if(userOrAdmin != null)
            {
                if (userOrAdmin.IsAdmin)
                {
                    return Ok(new { role = "admin" });
                }
                else
                {
                    return Ok(new { role = "user", userId = userOrAdmin.UserID });
                }
            }
            else
            {
                return BadRequest("Invalid Email or Password");
            }
        }
        [HttpPost]
        [Route("api/user/Register")]
        public IHttpActionResult PostRegisterNewUser(string email,string password)
        {
            UserDTO userDTO = new UserDTO
            {
                Email = email,
                Password = password
            };
            return Ok();
        }
        [HttpGet]
        [Route("api/user/{id}/home")]
        public IHttpActionResult GetAllFoodItems()
        {
            UserService userService = new UserService();
            List<FoodItemDTO> foodItemDTOs = userService.GetAllFoodItems();
            List<FoodItemModel> foodItemModels = _mapper.Map<List<FoodItemModel>>(foodItemDTOs);
            return Ok(foodItemModels);
        }
        [HttpGet]
        [Route("user/{userId}/order-history")]
        public IHttpActionResult GetOrderHistory(int userId)
        {
            UserService userService = new UserService();
            UserOrderHistory userOrderHistory = userService.GetOrderHistory(userId);
            UserOrderHistoryModel userOrderHistoryModel = _mapper.Map<UserOrderHistoryModel>(userOrderHistory);
            if(userOrderHistoryModel != null)
            {
                return Ok(userOrderHistoryModel);
            }
            else
            {
                return Ok<string>("No previous order available");
            }
        }
        // https://localhost:44338/api/user/getseacrchresult?search=rice
        [HttpGet]
        public IHttpActionResult GetSearchResults(string search)
        {
            UserService userService = new UserService();
            List<FoodItemDTO> foodItemDTOs = userService.GetSearchResults(search);
            if (foodItemDTOs.Any())
            {
                List<FoodItemModel> foodItemModels = _mapper.Map<List<FoodItemModel>>(foodItemDTOs);
                return Ok(foodItemModels);
            }
            else
            {
                return Ok<string>("No product found");
            }
        }
    }
}
