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
    public class ReviewsController : Controller
    {
        private ReviewDBContext db;
        
        public ReviewsController(ReviewDBContext db)
        {
            this.db = db;
        }


        [HttpPost]
        [ActionName("SubmitReview")]
        public IActionResult SubmitReview([FromForm] String game, [FromForm] String content)
        {
            String email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                String id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
                String sender = HttpContext.Session.GetString("email");
                String datetime = DateTime.Now.ToString();

                Review rev = new Review(id, game, sender, content, datetime);

                db.Add(rev);
                db.SaveChanges();

               
            }
            else
            {
                TempData["msg"] = "<script>alert('Va rugam sa va logati pentru a lasa comentarii');</script>";
            }           
            return RedirectToAction("CurrentProduct", "CurrentProduct", new { productName = game });
        }

        [HttpGet]
        [ActionName("GetReviews")]
        public IEnumerable<Review> GetReviews()
        {
            return db.Reviews.AsEnumerable();
        }

    }

}