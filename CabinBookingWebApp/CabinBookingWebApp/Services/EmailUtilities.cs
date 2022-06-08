namespace CabinBookingWebApp.Services
{
    public static class EmailUtilities
    {
        public static string SubjectBookingSubmited = "Booking Request Received";
        public static string ContentBookingSubmited = "We received your booking reservation. We will confirm as soon as possible";
        public static string SubjectBookingEdited = "Booking confirmation";
        public static string ContentBookingEdited(string statusEdited)
        {
            return "Your booking was" + statusEdited;
        }

        
    }
}
