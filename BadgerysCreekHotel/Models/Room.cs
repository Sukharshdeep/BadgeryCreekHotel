using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Models
{
    public class Room
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"([G1-3]{1})$", ErrorMessage = "Level can either be G,1,2 or 3 only")]
        public string Level { get; set; }
        [RegularExpression(@"([1-3]{1})$", ErrorMessage = "Bed Count can either be 1, 2 or 3 only")]
        public int BedCount { get; set; }
        [Range(50,300)]
        public decimal Price { get; set; }
        //navigation properties
        public ICollection<Booking> TheBookings { get; set; }
    }
}
