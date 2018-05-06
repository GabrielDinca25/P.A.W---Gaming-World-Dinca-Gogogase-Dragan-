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

        [HttpGet]
        public IActionResult AddGameToDatabase()
        {
            String name = HttpContext.Request.Query["name"].ToString();
            String keyPrice = HttpContext.Request.Query["keyPrice"].ToString();
            String hardPrice = HttpContext.Request.Query["hardPrice"].ToString();
            String platform = HttpContext.Request.Query["platform"].ToString();
            String image = HttpContext.Request.Query["image"].ToString();

            Game game = new Game(name, keyPrice, hardPrice, platform, image);
            db.Games.Add(game);
            db.SaveChanges();

            return RedirectToAction("Admin", ControllerName);
        }
    }
}