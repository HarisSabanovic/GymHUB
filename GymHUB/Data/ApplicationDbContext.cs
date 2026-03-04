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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InstructorRoom>()
                .HasKey(ir => new { ir.InstructorId, ir.RoomId });

            builder.Entity<InstructorRoom>()
           .HasOne(ir => ir.Instructor)
           .WithMany(i => i.InstructorRooms)
           .HasForeignKey(ir => ir.InstructorId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InstructorRoom>()
                .HasOne(ir => ir.Room)
                .WithMany(r => r.InstructorRooms)
                .HasForeignKey(ir => ir.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Room 1..* ClassSessions 
            builder.Entity<ClassSession>()
                .HasOne(cs => cs.Room)
                .WithMany(r => r.ClassSessions)
                .HasForeignKey(cs => cs.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Instructor 1..* ClassSessions 
            builder.Entity<ClassSession>()
                .HasOne(cs => cs.Instructor)
                .WithMany(i => i.ClassSessions)
                .HasForeignKey(cs => cs.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ClassSession 1..* Bookings 
            builder.Entity<Booking>()
                .HasOne(b => b.ClassSession)
                .WithMany(cs => cs.Bookings)
                .HasForeignKey(b => b.ClassSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // User 1..* Bookings
            builder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // hindrar dubbelbokning
            builder.Entity<Booking>()
                .HasIndex(b => new { b.UserId, b.ClassSessionId })
                .IsUnique();
        }

    }
}
