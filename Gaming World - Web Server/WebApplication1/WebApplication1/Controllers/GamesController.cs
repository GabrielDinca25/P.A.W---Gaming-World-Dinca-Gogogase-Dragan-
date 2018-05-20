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

        [HttpGet]
        [ActionName("RemoveProduct")]
        public IActionResult RemoveProduct()
        {
            String productName = HttpContext.Request.Query["productName"].ToString();

            var productToRemove = db.Games.Where(g => g.Name.Equals(productName)).FirstOrDefault();

            if(productToRemove != null)
            {
                db.Remove(productToRemove);
                db.SaveChanges();
            }

            return RedirectToAction("Admin", "Home");
        }

        [HttpGet]
        [ActionName("UpdateProduct")]
        public IActionResult UpdateProduct()
        {
            String productName = HttpContext.Request.Query["productName"].ToString();
            String newKeyPrice = HttpContext.Request.Query["keyPrice"].ToString();
            String newHardPrice = HttpContext.Request.Query["hardPrice"].ToString();

            var productToUpdate = db.Games.Where(g => g.Name.Equals(productName)).FirstOrDefault();

            Single keyPriceInt, hardPriceInt;
            bool kpresult = Single.TryParse(newKeyPrice, out keyPriceInt);
            bool hpresult = Single.TryParse(newKeyPrice, out hardPriceInt); 

            if (productToUpdate != null && kpresult && hpresult)
            {
                productToUpdate.KeyPrice = keyPriceInt > 0 ? newKeyPrice : productToUpdate.KeyPrice;
                productToUpdate.HardPrice = hardPriceInt > 0 ? newHardPrice : productToUpdate.HardPrice;
                db.SaveChanges();
            }
            else
            {
                TempData["msg"] = "<script>alert('Invalid input');</script>";
            }

            return RedirectToAction("Admin", "Home");
        }

    }
}