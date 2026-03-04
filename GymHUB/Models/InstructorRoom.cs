namespace GymHUB.Models
{
    public class InstructorRoom
    {
        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }


        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
