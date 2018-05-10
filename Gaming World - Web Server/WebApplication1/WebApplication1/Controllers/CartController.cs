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

            var currentGame = from g in db.Games
                               where g.Name.Equals(gameName)
                               select new Game(g.Name, g.KeyPrice, g.HardPrice, g.Platform, g.Image, g.Genre, g.Amount);

            foreach(Game game in currentGame)
            {
                cart_db.CartProducts.Add(game);
                cart_db.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("{id}", Name = "Add")]
        public IEnumerable<Game> Add(int id)
        {
            return from g in cart_db.CartProducts select new Game { Name = g.Name, KeyPrice = g.KeyPrice, HardPrice = g.HardPrice, Platform = g.Platform, Image = g.Image };
        }
    }
}