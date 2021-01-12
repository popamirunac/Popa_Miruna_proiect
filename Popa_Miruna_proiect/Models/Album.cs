using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Popa_Miruna_proiect.Models
{
    public class Album
    {
        public int ID { get; set; }
        [Required] 
        [Display(Name = "Album Name")] 
        [StringLength(50)] 
        public string AlbumName { get; set; }
        [StringLength(70)] 
        public string Year { get; set; }
        public ICollection<AlbumSong> AlbumSongs { get; set; }
    }
}
