using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public String Username { get; set; }
        public String Password { get; set; }

        public User() { }

        public User(String username, String password)
        {
            Username = username;
            Password = password;
        }

        public class UserDBContext : DbContext
        {
            public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<User>().ToTable("UserCredentials");
            }

            public DbSet<User> UserCredentials { get; set; }
        }
    }
}

