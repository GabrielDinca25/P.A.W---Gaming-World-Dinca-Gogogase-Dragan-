using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationsController : Controller
    {
        private ApplicationDBContext db;

        public ApplicationsController(ApplicationDBContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public String Get()
        {
            String subject = HttpContext.Request.Query["subject"].ToString();
            String details = HttpContext.Request.Query["details"].ToString();
            Application app = new Application("Alipire", subject, "Nu am", "gdinca");
            db.Applications.Add(app);
            db.SaveChanges();

            var applications = from a in db.Applications
                               orderby a.Subject
                               select a;

            String entries = "<b>The current applications are:</b> \n";
            foreach (var a in applications)
            {
                entries += a.Type + " " + a.Subject + " " + a.Details + " " + a.Sender + "\n";
            }

            return entries;
            //return subject;

        }

    }
}