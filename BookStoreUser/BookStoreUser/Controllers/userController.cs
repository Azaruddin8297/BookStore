using BookStore.User.Entity;
using BookStore.User.Interface;
using BookStore.User.Model;
using BookStore.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserServices _user;
        public ResponseEntity response;
        public userController(IUserServices user)
        {
            _user = user;
            response = new ResponseEntity();
        }
        [HttpPost]
        [Route("Register")]
        public ResponseEntity Register(UserModel newUser)
        {
            UserEntity user = _user.Register(newUser);
            if (user != null)
            {
                response.Data = user;

            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong";
            }
            return response;
        }
        [HttpPost]
        [Route("Login")]
        public ResponseEntity Login(string email, string password)
        {
            string user = _user.Login(email, password);
            if (user != null)
            {
                response.Data = user;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong";
            }
            return response;
        }
        [HttpGet]
        [Route("ForgetPassword")]
        public ResponseEntity ForgetPassword(string email)
        {
            bool user = _user.ForgetPassword(email);
            if (user)
            {
                response.Data = user;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong";
            }
            return response;
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ResponseEntity ResetPassword(string newPassword, string comfirmPassword)
        {
            string emailId = User.FindFirstValue(ClaimTypes.Email);
            bool result = _user.ResetPassword(newPassword, comfirmPassword,emailId);
            if (result)
            {
                response.Data = result;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong";
            }
            return response;
        }
        [Authorize]
        [HttpGet]
        [Route("GetUser")]
        public ResponseEntity GetUser()
        {
            int result = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            var user = _user.GetUser(result);
            if (user != null)
            {
                response.Data = user;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Something Went Wrong";
            }
            return response;
        }
    }
}
