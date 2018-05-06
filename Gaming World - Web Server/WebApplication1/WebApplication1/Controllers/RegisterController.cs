using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using static WebApplication1.Models.User;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private UserDBContext db;

        public RegisterController(UserDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {

            String firstName = HttpContext.Request.Query["firstName"].ToString();
            String lastName = HttpContext.Request.Query["lastName"].ToString();
            //String accountType = HttpContext.Request.Query["accountType"].ToString();
            String email = HttpContext.Request.Query["email"].ToString();
            String password = HttpContext.Request.Query["password"].ToString();
            String confirmedPassword = HttpContext.Request.Query["confirmedPassword"].ToString();
            

            if (firstName.Trim() != "" && (password.Length > 5 && password.Trim() != "" &&
                password.Equals(confirmedPassword)) )
            {
                User user = new User (firstName, lastName, email, password);
                db.UserCredentials.Add(user); 
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}