using AutoMapper;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IMapper _mapper;
        public AdminController()
        {
            AutoMapperConfigAPI.Initialize();
            _mapper = AutoMapperConfigAPI.Mapper;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpDelete]
        [Route("deletefooditem/{id}")]
        public IHttpActionResult DeleteFoodItem(int id)
        {
            AdminService adminService = new AdminService();
            bool result = adminService.DeleteFoodItem(id);
            if (result)
            {
                return Ok<string>("FoodItem successfully deleted");
            }
            else
            {
                return Ok<string>("Error occured Try Again...");
            }
        }

        [HttpPost]
        [Route("addfooditem")]
        public IHttpActionResult AddFoodItem(AddFoodItemModel addFoodItemModel)
        {
            AdminService adminService = new AdminService();
            AddFoodItemModelDTO addFoodItemModelDTO = _mapper.Map<AddFoodItemModelDTO>(addFoodItemModel);
            bool result = adminService.AddFoodItem(addFoodItemModelDTO);
            if (result)
            {
                return Ok<string>("FoodItem successfully added");
            }
            else
            {
                return Ok<string>("Error occured Try Again...");
            }
        }

        [HttpGet]
        [Route("getallcategory")]
        public IHttpActionResult GetAllCategory()
        {
            AdminService adminService = new AdminService();
            List<CategoryDTO> categoryDTOs = adminService.GetAllCategory();
            List<CategoryModel> categoryModels = _mapper.Map<List<CategoryModel>>(categoryDTOs);
            if(categoryModels != null)
            {
                List<string> categoryNames = categoryModels.Select(c => c.CategoryName).ToList();
                return Ok(categoryNames);
            }
            else
            {
                return Ok<string>("Something went wrong, try again");
            }
        }
        [HttpPost]
        [Route("changepassword")]
        public IHttpActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            AdminService adminService = new AdminService();
            ChangePasswordDTO changePassword = new ChangePasswordDTO();
            changePassword.Email = changePasswordModel.Email;
            changePassword.NewPassword = changePasswordModel.NewPassword;
            if (adminService.ChangePassword(changePassword.Email, changePassword.NewPassword))
            {
                return Ok<string>("Password changed successfully");
            }
            else
            {
                return Ok<string>("Error occured...Try Again");
            }
        }
        [HttpPost]
        [Route("registereduserlogin")]
        public IHttpActionResult PostRegisterUserLogin(string email, string password)
        {
            UserService userService = new UserService();
            UserDTO userOrAdmin = userService.AuthenticateUser(email, password);
            if (userOrAdmin != null)
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
        // https://localhost:44338/api/admin/id/DeleteFoodItem
    }
}
