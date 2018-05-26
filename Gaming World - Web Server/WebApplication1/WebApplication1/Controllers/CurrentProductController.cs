using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CurrentProductController : Controller
    {
        private GameDBContext db;

        public CurrentProductController(GameDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [ActionName("CurrentProduct")]
        public IActionResult CurrentProduct()
        {
            String productName = HttpContext.Request.Query["productName"].ToString();

            var currentProduct = db.Games.Where(g => g.Name.Equals(productName)).FirstOrDefault();

            if(currentProduct != null)
            {
                ViewData["currentProduct"] = currentProduct;
                return View();
            } 
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }
    }
}