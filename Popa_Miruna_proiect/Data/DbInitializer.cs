using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Popa_Miruna_proiect;
using Popa_Miruna_proiect.Models;

namespace Popa_Miruna_proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(SpotifyContext context)
        {
            context.Database.EnsureCreated();

            if (context.Songs.Any())
            {
                return;
            }
            var songs = new Song[]
            {
                new Song{Title="Faith",Singer="The Weeknd",Price=Decimal.Parse("22")},
                new Song{Title="Sorry",Singer="Beyonce",Price=Decimal.Parse("18")},
                new Song{Title="No Guidance",Singer="Chris Brown",Price=Decimal.Parse("27")},
                new Song{Title="Snowchild",Singer="The Weeknd",Price=Decimal.Parse("45")},
                new Song{Title="Overtime",Singer="Chris Brown",Price=Decimal.Parse("38")}, 
                new Song{Title="Alone Again",Singer="The Weeknd",Price=Decimal.Parse("28")},
            };
            foreach (Song s in songs)
            {
                context.Songs.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {
                new Customer{CustomerID=1050,Name="Bradu Amalia",Email="braduamalia@yahoo.com"},
                new Customer{CustomerID=1045,Name="Calugareanu Razvan",Email="razvancr@yahoo.com"},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges(); 

            var orders = new Order[] 
            {
                new Order{SongID=1,CustomerID=1050,OrderDate=DateTime.Parse("02-25-2020")},
                new Order{SongID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{SongID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2020")},
                new Order{SongID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")}, 
                new Order{SongID=4,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2020")},
                new Order{SongID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2020")},
            };
            foreach (Order e in orders) 
            {
                context.Orders.Add(e); 
            }
            context.SaveChanges();

            var albums = new Album[]
            {
                new Album { AlbumName = "INDIGO", Year = "2019" },
                new Album { AlbumName = "Lemonade", Year = "2016" },
                new Album { AlbumName = "After Hours", Year = "2020" },
            };
            foreach (Album p in albums) 
            {
                context.Albums.Add(p); }
            context.SaveChanges();
            
            var albumsongs = new AlbumSong[]
            {
                new AlbumSong { SongID = songs.Single(c => c.Title == "No Guidance" ).ID, 
                    AlbumID = albums.Single(i => i.AlbumName == "INDIGO").ID}, 
                new AlbumSong {SongID = songs.Single(c => c.Title == "Sorry" ).ID, 
                    AlbumID = albums.Single(i => i.AlbumName == "Lemonade").ID}, 
                new AlbumSong {SongID = songs.Single(c => c.Title == "Faith" ).ID,
                    AlbumID = albums.Single(i => i.AlbumName == "After Hours").ID}, 
                new AlbumSong {SongID = songs.Single(c => c.Title == "Snowchild" ).ID,
                    AlbumID = albums.Single(i => i.AlbumName == "After Hours").ID}, 
                new AlbumSong {SongID = songs.Single(c => c.Title == "Overtime" ).ID, 
                    AlbumID = albums.Single(i => i.AlbumName == "INDIGO").ID}, 
                new AlbumSong { SongID = songs.Single(c => c.Title == "Alone Again" ).ID,
                    AlbumID = albums.Single(i => i.AlbumName == "After Hours"  ).ID },
            };
            foreach (AlbumSong pb in albumsongs) { context.AlbumSongs.Add(pb); }
            context.SaveChanges();
        }
    }
}
