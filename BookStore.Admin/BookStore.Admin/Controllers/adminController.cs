using Azure;
using BookStore.Admin.Entity;
using BookStore.Admin.Interfaces;
using BookStore.Admin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private readonly ResponseEntity response;
        private readonly AdminContext _db;
        public adminController(IAdmin admin, AdminContext db)
        {
            _admin = admin;
            response = new ResponseEntity();
            _db = db;

        }

        [HttpPost]
        [Route("register")]
        public ResponseEntity RegisterUser(AdminModel newUser)
        {
            try
            {
                AdminEntity admin = _admin.RegisterAdmin(newUser);

                if (admin != null)
                {
                    response.Data = admin;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public ResponseEntity Login(string email, string password)
        {
            try
            {

                string result = _admin.AdminLogin(email, password);

                if (result != null)
                {
                    response.Data = result;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
