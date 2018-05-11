using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult Add()
        {
            String gameName = HttpContext.Request.Query["gameName"].ToString();

            var currentGame = db.Games
                               .Where(game => game.Name.Equals(gameName))
                               .FirstOrDefault(); 
            if (currentGame != null)
            { 
                cart_db.CartProducts.Add(currentGame);
                cart_db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("{id}", Name = "Add")]
        public IEnumerable<Game> Add(int id)
        {
            return cart_db.CartProducts.AsEnumerable();
            //return from g in cart_db.CartProducts select new Game { Name = g.Name, KeyPrice = g.KeyPrice, HardPrice = g.HardPrice, Platform = g.Platform, Image = g.Image };
        }
    }
}