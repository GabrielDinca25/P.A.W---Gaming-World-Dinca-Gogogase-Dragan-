using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using static WebApplication1.Models.User;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LoginController : Controller
    {
        private UserDBContext db;

        public LoginController(UserDBContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login([FromForm] String email, [FromForm]String password)
        {
           // String email = HttpContext.Request.Query["email"].ToString();
           // String password = HttpContext.Request.Query["password"].ToString();
            var users = db.UserCredentials;

            foreach (var user in users)
            {
                if (user.Email.Equals(email))
                {
                    if (user.Password.Equals(password))
                    {
                        HttpContext.Session.SetString("email", email);
                        if (email.StartsWith("admin"))
                        {
                            return RedirectToAction("Admin", "Home");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            TempData["msg"] = "<script>alert('Incorrect username and password');</script>";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("GetLoggedInUser")]
        public IEnumerable<User> GetLoggedInUser()
        {
            String email = HttpContext.Session.GetString("email");
            User user = new User(email, "encrypted", "encrypted", "encrypted");
            List<User> usernameFakeList = new List<User>();
            usernameFakeList.Add(user);
            return usernameFakeList.AsEnumerable();
        }

        [HttpGet]
        [ActionName("LogOut")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return db.UserCredentials.AsEnumerable();
        }

        [HttpGet]
        [ActionName("GetUserInfo")]
        public IEnumerable<User> GetUserInfo()
        {
            String email = HttpContext.Session.GetString("email");
            var data = from u in db.UserCredentials
                        orderby u.Email
                        select u;
            foreach (var dataSelected in data)
            {
                if (dataSelected.Email.Equals(email))
                {
                    List<User> usernameFakeList = new List<User>();
                    usernameFakeList.Add(dataSelected);
                    return usernameFakeList.AsEnumerable();
                   
                }
            }

            List<User> usernameFakeList2 = new List<User>();
            return usernameFakeList2.AsEnumerable();
        }

        [HttpGet]
        [ActionName("UpdateProfile")]
        public IActionResult UpdateProfile()
        {
            String firstName = HttpContext.Request.Query["firstName"].ToString();
            String lastName = HttpContext.Request.Query["lastName"].ToString();
            String email = HttpContext.Request.Query["email"].ToString();
            String password = HttpContext.Request.Query["password"].ToString();

            var userToUpdate = db.UserCredentials.Where(g => g.Email.Equals(HttpContext.Session.GetString("email"))).FirstOrDefault();
            

            if (userToUpdate != null )
            {
                db.Remove(userToUpdate);
                db.SaveChanges();
                User updateUser = new User(email,firstName,lastName,password);
                db.Add(updateUser);
                db.SaveChanges();
                return LogOut();
            }
            else
            {
                TempData["msg"] = "<script>alert('Invalid input');</script>";
                return RedirectToAction("Profile", "Home");
            }

            
        }

    }
}