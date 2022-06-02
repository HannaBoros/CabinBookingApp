using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CabinBookingWebApp.Models
{
    public class Cabin
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }
        public int Rooms { get; set; }
        public int Price { get; set; }
       

        public string? MainImageUrl { get; set; }
   

        public ICollection<Booking>? Bookings { get; set; }
                            
    }
}