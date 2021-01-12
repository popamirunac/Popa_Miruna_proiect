using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Popa_Miruna_proiect.Models.SpotifyViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)] 
        public DateTime? OrderDate { get; set; }
        public int SongCount { get; set; }
    }
}
