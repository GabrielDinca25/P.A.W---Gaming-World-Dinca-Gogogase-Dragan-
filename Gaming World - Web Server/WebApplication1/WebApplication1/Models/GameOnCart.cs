using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class GameOnCart
    {
        [Key]
        public String Name { get; set; }
        public String Email { get; set; }
        public String KeyPrice { get; set; }
        public String HardPrice { get; set; }
        public String Platform { get; set; }
        public String Image { get; set; }
        public String Genre { get; set; }
        public String Amount { get; set; }

        public GameOnCart()
        {
        }

        public GameOnCart(String email, String name, String key, String hard, String platform, String image, String genre, String amount)
        {
            Email = email;
            Name = name;
            KeyPrice = key;
            HardPrice = hard;
            Platform = platform;
            Image = image;
            Genre = genre;
            Amount = amount;
        }
    }

    public class CartProductDBContext : DbContext
    {
        public CartProductDBContext(DbContextOptions<CartProductDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameOnCart>().ToTable("CartProduct");
        }

        public DbSet<GameOnCart> CartProducts { get; set; }
    }
}
