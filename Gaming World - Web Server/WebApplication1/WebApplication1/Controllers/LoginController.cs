using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static WebApplication1.Models.User;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private UserDBContext db;

        public LoginController(UserDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult Get()
        {
            String username = HttpContext.Request.Query["username"].ToString();
            String password = HttpContext.Request.Query["password"].ToString();

            var users = from u in db.UserCredentials
                        orderby u.Username
                        select u;


            foreach (var user in users)
            {
                if (user.Username.Equals(username))
                {
                    if (user.Password.Equals(password))
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                }
            }

            TempData["msg"] = "<script>alert('Incorrect username and password');</script>";
            return RedirectToAction("Index", "Home");

            //if (username.Equals("gdinca") && password.Equals("slatina"))
            //{
            //    return RedirectToAction("HomePage", "Home");
            //}
            //else
            //{
            //    return RedirectToAction("Error", "Home");
            //}


        }
    }
}