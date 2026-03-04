namespace GymHUB.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string? Name { get; set; }
        public int MaxCapacity { get; set; }

        public ICollection<ClassSession> ClassSessions { get; set; } = new List<ClassSession>();
        public ICollection<InstructorRoom> InstructorRooms { get; set; } = new List<InstructorRoom>();
    }
}
