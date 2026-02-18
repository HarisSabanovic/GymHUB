using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymHUB.Models;

namespace GymHUB.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //tabeller
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<InstructorRoom> InstructorRooms { get; set; }
        public DbSet<ClassSession> ClassSessions { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
