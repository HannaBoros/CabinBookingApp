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
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(AutoGenerateField = false)]
        public int CabinId { get; set; }

        [ForeignKey("CabinId")]
        public Cabin Cabin { get; set; }
    }
}
