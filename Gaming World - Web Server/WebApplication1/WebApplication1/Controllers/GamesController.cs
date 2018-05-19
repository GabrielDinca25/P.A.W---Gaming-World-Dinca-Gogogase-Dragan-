using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GamesController : Controller
    {
        private const string ControllerName = "Home";
        private GameDBContext db;

        public GamesController(GameDBContext db)
        {
            this.db = db;
        }


        [HttpPost]
        [ActionName("AddGame")]
        public async Task<IActionResult> AddGame([FromForm] Game gameToAdd)
        {
            await db.Games.AddAsync(gameToAdd);
            db.SaveChanges();
            return RedirectToAction("Admin", ControllerName);
        }


        [HttpGet("{id}", Name = "Get")]
        [ActionName("GetGames")]
        public IEnumerable<Game> GetGames(int id)
        {
            String platform = "";
            switch (id)
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
                    return db.Games.AsEnumerable();
                    //break;
            }

            return db.Games.Where(g => g.Platform.Equals(platform)).AsEnumerable();
        }

        [HttpGet]
        [ActionName("GetSearchResults")]
        public IEnumerable<Game> GetSearchResults()
        {
            String searchTerm = HttpContext.Request.Query["search"].ToString();
            return db.Games.Where(g => g.Name.Contains(searchTerm)).AsEnumerable();
        }
    }
}