using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CartController : Controller
    {
        private GameDBContext db;
        private CartProductDBContext cart_db;

        public CartController(GameDBContext db, CartProductDBContext cart_db)
        {
            this.db = db;
            this.cart_db = cart_db;
        }

        [HttpGet]
        [ActionName("Add")]
        public IActionResult Add()
        {
            String email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                String gameName = HttpContext.Request.Query["gameName"].ToString();

                var currentGame = db.Games
                                   .Where(game => game.Name.Equals(gameName))
                                   .FirstOrDefault();
                if (currentGame != null)
                {
                    if (!CheckDuplicate(currentGame))
                    {
                        cart_db.CartProducts.Add(currentGame);
                        cart_db.SaveChanges();
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "<script>alert('Va rugam sa va logati');</script>";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [ActionName("GetCartProducts")]
        public IEnumerable<Game> GetCartProducts(int id)
        {
            return cart_db.CartProducts.AsEnumerable();
            //return from g in cart_db.CartProducts select new Game { Name = g.Name, KeyPrice = g.KeyPrice, HardPrice = g.HardPrice, Platform = g.Platform, Image = g.Image };
        }

        [HttpGet]
        [ActionName("Remove")]
        public ActionResult Remove()
        {
            String gameName = HttpContext.Request.Query["gameName"].ToString();

            var currentGame = cart_db.CartProducts
                               .Where(game => game.Name.Equals(gameName))
                               .FirstOrDefault();
            if (currentGame != null)
            {
                cart_db.Remove(currentGame);
                cart_db.SaveChanges();
            }

            return RedirectToAction("ShoppingCart", "Home");
        }

        [HttpGet]
        [ActionName("Checkout")]
        public ActionResult Checkout()
        {
            
            foreach (var game in cart_db.CartProducts)
            {
                cart_db.CartProducts.Remove(game);
            }
            cart_db.SaveChanges();
            return RedirectToAction("ShoppingCart", "Home");
        }

        [HttpGet]
        [ActionName("GetTotalPrice")]
        public ActionResult GetTotalPrice()
        {

            foreach (var game in cart_db.CartProducts)
            {
                cart_db.CartProducts.Remove(game);
            }
            cart_db.SaveChanges();
            return RedirectToAction("ShoppingCart", "Home");
        }

        public bool CheckDuplicate(Game game)
        {

            var games = from u in cart_db.CartProducts
                        orderby u.Name
                        select u;
            foreach (var gameName in games)
            {
                if (gameName.Name.Equals(game.Name))
                {
                    TempData["msg"] = "<script>alert('Incorrect username already exist');</script>";
                    return true;
                }
            }
            return false;
        }
    }
}