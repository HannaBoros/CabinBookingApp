using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabinBookingWebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Required]
        [Display(Name= "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } 
        
        public ICollection<Booking> Bookings { get; set; }
    }
}
