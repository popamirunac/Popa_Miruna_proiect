using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Popa_Miruna_proiect.Models;

namespace Popa_Miruna_proiect.Data
{
    public class SpotifyContext : DbContext 
    { 
        public SpotifyContext(DbContextOptions<SpotifyContext> options) : base(options) 
        { 
        }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Order> Orders { get; set; } 
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumSong> AlbumSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Customer>().ToTable("Customer"); 
            modelBuilder.Entity<Order>().ToTable("Order"); 
            modelBuilder.Entity<Song>().ToTable("Song");
            modelBuilder.Entity<Album>().ToTable("Album"); 
            modelBuilder.Entity<AlbumSong>().ToTable("AlbumSong"); 
            modelBuilder.Entity<AlbumSong>().HasKey(c => new { c.SongID, c.AlbumID });//configureaza cheia primara compusa
        }
    }
}
