﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Game
    {
        [Key]
        public String Name { get; set; }
        public String KeyPrice { get; set; }
        public String HardPrice { get; set; }
        public String Platform { get; set; }
        public String Image { get; set; }

        public Game(String name, String key, String hard, String platform, String image)
        {
            Name = name;
            KeyPrice = key;
            HardPrice = hard;
            Platform = platform;
            Image = image;
        }
    }

    public class GameDBContext : DbContext
    {
        public GameDBContext(DbContextOptions<GameDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Game>().ToTable("Games");
        }

        public DbSet<Game> Games { get; set; }
    }

}
