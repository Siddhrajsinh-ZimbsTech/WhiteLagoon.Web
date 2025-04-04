using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteLagoon.Domain.Entites
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }
        public  string Name { get; set; }
        [ForeignKey("Villa")]
        public int VillaId {  get; set; }
        public Villa? Villa { get; set;  }
        public string? Description { get; set; } 
    }
}
