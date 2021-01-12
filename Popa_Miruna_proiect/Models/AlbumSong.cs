using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Popa_Miruna_proiect.Models
{
    public class AlbumSong
    {
        public int AlbumID { get; set; }
        public int SongID { get; set; }
        public Album Album { get; set; }
        public Song Song { get; set; }
    }
}
