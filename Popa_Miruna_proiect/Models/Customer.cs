using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Popa_Miruna_proiect.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
