using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BadgerysCreekHotel.Models
{
    public class Customer
    {
        [Key,Required,EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; }
        [Required, MinLength(2), MaxLength(20)]
        [RegularExpression(@"[a-zA-z-' ]+$", ErrorMessage = "This field can only have english letters, hyphen ,apostrophe and minimum length is 2 and maximum length is 20")]
        public string Surname { get; set; }
        [Required, MinLength(2), MaxLength(20)]
        [RegularExpression(@"[a-zA-z-' ]+$", ErrorMessage = "This field can only have english letters, hyphen ,apostrophe and minimum length is 2 and maximum length is 20")]
        public string GivenName { get; set; }
        [Required]
        [RegularExpression(@"([0-9]{4})$", ErrorMessage = "PostCode  should only have 4 digits only")]
        public string Postcode { get; set; }
        public ICollection<Booking> TheBookings { get; set; }
    }
}
