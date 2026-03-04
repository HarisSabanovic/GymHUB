namespace GymHUB.Models
{
    public class Booking
    {
        public int BookingId { get; set; }


        //låter mig komma åt hela objektet istället för bara ID
        public int ClassSessionId { get; set; }
        public ClassSession ClassSession { get; set; } = null!;
        
       
        public string? UserId { get; set; }
        public User? User { get; set; } 


        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public string Status { get; set; } = "Booked";

    }
}
