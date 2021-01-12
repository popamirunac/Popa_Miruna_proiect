using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Popa_Miruna_proiect.Models.SpotifyViewModels
{
    public class AlbumIndexData
    {
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
