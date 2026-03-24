using GymHUB.Data;
using GymHUB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymHUB.Controllers
{
    [Authorize] // allt här kräver login
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public BookingsController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // Mina bokningar
        public async Task<IActionResult> My()
        {
            var userId = _userManager.GetUserId(User);

            var bookings = await _db.Bookings
                .Include(b => b.ClassSession)
                    .ThenInclude(cs => cs.Room)
                .Include(b => b.ClassSession)
                    .ThenInclude(cs => cs.Instructor)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View(bookings);
        }

        // Boka ett pass
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int classSessionId)
        {
            var userId = _userManager.GetUserId(User);

            var session = await _db.ClassSessions
                .Include(s => s.Room)
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.ClassSessionId == classSessionId);

            if (session == null) return NotFound();

            // Fullbokad
            var bookedCount = session.Bookings.Count(b => b.Status == "Booked");
            if (bookedCount >= session.Room.MaxCapacity)
            {
                TempData["Error"] = "Passet är fullbokat.";
                return RedirectToAction("Details", "ClassSessions", new { id = classSessionId });
            }

            // Dubbelbokning
            var alreadyBooked = await _db.Bookings.AnyAsync(b =>
                b.UserId == userId &&
                b.ClassSessionId == classSessionId &&
                b.Status == "Booked");

            if (alreadyBooked)
            {
                TempData["Error"] = "Du har redan bokat detta pass.";
                return RedirectToAction("Details", "ClassSessions", new { id = classSessionId });
            }

            var booking = new Booking
            {
                UserId = userId,
                ClassSessionId = classSessionId,
                CreatedAt = DateTime.Now,
                Status = "Booked"
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Bokning skapad!";
            return RedirectToAction("My");
        }

        // Avboka (endast egna)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = _userManager.GetUserId(User);

            var booking = await _db.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null) return NotFound();

            if (booking.UserId != userId)
                return Forbid();

            _db.Bookings.Remove(booking);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Bokning avbokad.";
            return RedirectToAction("My");
        }
    }
}
