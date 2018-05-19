using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Application
    {
        [Key]
        public String Subject { get; set; }
        public String Type { get; set; }
        public String Details { get; set; }
        public String Sender { get; set; }

        public Application() { }

        public Application(String type, String subject, String details, String sender)
        {
            Type = type;
            Subject = subject;
            Details = details;
            Sender = sender;
        }
    }

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Application>().ToTable("Applications");
        }

        public DbSet<Application> Applications { get; set; }
    }
}
