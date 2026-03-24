using GymHUB.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GymHUB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBookingsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminBookingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Alla bokningar
        public async Task<IActionResult> Index()
        {
            var bookings = await _db.Bookings
                .Include(b => b.User)
                .Include(b => b.ClassSession)
                    .ThenInclude(cs => cs.Room)
                .Include(b => b.ClassSession)
                    .ThenInclude(cs => cs.Instructor)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View(bookings);
        }

        // Visa bokningar för ett specifikt pass
        public async Task<IActionResult> ForSession(int classSessionId)
        {
            var session = await _db.ClassSessions
                .Include(s => s.Room)
                .Include(s => s.Instructor)
                .FirstOrDefaultAsync(s => s.ClassSessionId == classSessionId);

            if (session == null) return NotFound();

            ViewBag.Session = session;

            var bookings = await _db.Bookings
                .Include(b => b.User)
                .Where(b => b.ClassSessionId == classSessionId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View(bookings);
        }
    }
}
