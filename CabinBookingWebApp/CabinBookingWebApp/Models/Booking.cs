using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabinBookingWebApp.Models
{
    public class Booking
    {
        [Display(AutoGenerateField = false)]
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        //[DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(5, 2)")]
        //public decimal Price { get; set; }
        public int Price { get; set; }

        [Display(AutoGenerateField = false)]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [Display(AutoGenerateField = false)]

        [ForeignKey("Cabin")]
        public int CabinId { get; set; }
        public virtual Cabin? Cabin { get; set; }

        public BookingStatus? Status { get; set; }
    }

    public enum BookingStatus
    {
        Submitted,
        Approved,
        Rejected,
        Expired
    }
}
