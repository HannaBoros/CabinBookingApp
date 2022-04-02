using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabinBookingWebApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }        

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }

        public User User { get; set; }
    }
}
