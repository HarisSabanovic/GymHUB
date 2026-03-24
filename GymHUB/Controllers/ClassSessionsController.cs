using GymHUB.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymHUB.Controllers
{
    public class ClassSessionsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ClassSessionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Publik lista
        public async Task<IActionResult> Index()
        {
            var now = DateTime.Now;

            var sessions = await _db.ClassSessions
                .Include(s => s.Room)
                .Include(s => s.Instructor)
                .Where(s => s.StartTime >= now)
                .OrderBy(s => s.StartTime)
                .ToListAsync();

            return View(sessions);
        }

        // Publik detalj
        public async Task<IActionResult> Details(int id)
        {
            var session = await _db.ClassSessions
                .Include(s => s.Room)
                .Include(s => s.Instructor)
                .Include(s => s.Bookings)
                .FirstOrDefaultAsync(s => s.ClassSessionId == id);

            if (session == null) return NotFound();

            return View(session);
        }
    }
}