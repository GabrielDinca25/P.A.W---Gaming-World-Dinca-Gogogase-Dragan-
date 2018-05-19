using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using static WebApplication1.Models.User;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RegisterController : Controller
    {
        private UserDBContext db;

        public RegisterController(UserDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [ActionName("Get")]
        public IActionResult Get()
        {

            String firstName = HttpContext.Request.Query["firstName"].ToString();
            String lastName = HttpContext.Request.Query["lastName"].ToString();
            String email = HttpContext.Request.Query["email"].ToString();
            String password = HttpContext.Request.Query["password"].ToString();
            String confirmedPassword = HttpContext.Request.Query["confirmedPassword"].ToString();
            

            if (firstName.Trim() != "" && (password.Length > 5 && password.Trim() != "" &&
                password.Equals(confirmedPassword)) )
            {
                User user = new User (firstName, lastName, email, password);

                var users = from u in db.UserCredentials
                            orderby u.Email
                            select u;
                foreach (var userEmail in users)
                {
                    if (userEmail.Email.Equals(email))
                    {
                        TempData["msg"] = "<script>alert('Incorrect username already exist');</script>";
                        return RedirectToAction("Index", "Home");
                    }
                }

                db.UserCredentials.Add(user); 
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "<script>alert('Please enter all the data required');</script>";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}