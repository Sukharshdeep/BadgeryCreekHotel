using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadgerysCreekHotel.Models
{
    public class SearchRooms
    {
        [Display(Name = "Room Bed Count")]
        
        public int roomBedCount { get; set; }
        [Display(Name = "Check In")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingCheckIn { get; set; }
        [Display(Name = "Check Out")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingCheckOut { get; set; }

    }
}
