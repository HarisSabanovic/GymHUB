namespace GymHUB.Models
{
    public class ClassSession
    {
        public int ClassSessionId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }


        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }


        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }


        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
