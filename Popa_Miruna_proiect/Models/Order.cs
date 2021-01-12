using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Popa_Miruna_proiect.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int SongID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Song Song { get; set; }
    }
}
