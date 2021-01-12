using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Popa_Miruna_proiect.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Singer { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<AlbumSong> AlbumSongs { get; set; }
    }
}
