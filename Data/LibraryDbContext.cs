using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;
using System.IO;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OnlineLibrary.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OnlineLibrary.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseNpgsql(connectionString);
        }


    }
}
