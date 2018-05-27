using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Review
    {
        [Key]
        public String Id { get; set; }
        public String Game { get; set; }
        public String Sender { get; set; }
        public String Content { get; set; }
        public String DateTime { get; set; }

        public Review() { }

        public Review(String id, String game, String sender, String content, String datetime)
        {
            Id = id;
            Game = game;
            Sender = sender;
            Content = content;
            DateTime = datetime;
        }
    }

    public class ReviewDBContext : DbContext
    {
        public ReviewDBContext(DbContextOptions<ReviewDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>().ToTable("Reviews");
        }

        public DbSet<Review> Reviews { get; set; }
    }
}
