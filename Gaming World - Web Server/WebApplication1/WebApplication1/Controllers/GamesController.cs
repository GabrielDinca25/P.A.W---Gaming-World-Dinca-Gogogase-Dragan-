using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private const string ControllerName = "Home";
        private GameDBContext db;

        public GamesController(GameDBContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Game gameToAdd)
        {
            await db.Games.AddAsync(gameToAdd);
            db.SaveChanges();
            return RedirectToAction("Admin", ControllerName);
        }
           
        [HttpGet]
        public IActionResult AddGameToDatabase()
        {
            String name = HttpContext.Request.Query["name"].ToString();
            String keyPrice = HttpContext.Request.Query["keyPrice"].ToString();
            String hardPrice = HttpContext.Request.Query["hardPrice"].ToString();
            String platform = HttpContext.Request.Query["platform"].ToString();
            String image = HttpContext.Request.Query["image"].ToString();
            String genre = HttpContext.Request.Query["genre"].ToString();
            String amount = HttpContext.Request.Query["amount"].ToString();

            Game game = new Game(name, keyPrice, hardPrice, platform, image, genre, amount);
            db.Games.Add(game);
            db.SaveChanges();

            return RedirectToAction("Admin", ControllerName);

        }

        [HttpGet("{id}", Name = "Get")]
        public IEnumerable<Game> GetGames(int id)
        {
            String platform="";
            switch(id)
            {
                case 1:
                    platform = "PC";
                    break;
                case 2:
                    platform = "PS4";
                    break;
                case 3:
                    platform = "Xbox One";
                    break;
                case 4:
                    platform = "Switch";
                    break;
                case 5:
                    return from g in db.Games select new Game { Name = g.Name, KeyPrice = g.KeyPrice, HardPrice = g.HardPrice, Platform = g.Platform, Image = g.Image, Genre=g.Genre, Amount=g.Amount};
                    //break;
            }

            return from g in db.Games where g.Platform.Equals(platform) select new Game { Name = g.Name, KeyPrice = g.KeyPrice, HardPrice = g.HardPrice, Platform = g.Platform, Image = g.Image, Genre = g.Genre, Amount = g.Amount };
        }
    }
}