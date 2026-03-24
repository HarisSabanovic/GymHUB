using GymHUB.Data;
using GymHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymHUB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminClassSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminClassSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminClassSessions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassSessions.Include(c => c.Instructor).Include(c => c.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminClassSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions
                .Include(c => c.Instructor)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.ClassSessionId == id);
            if (classSession == null)
            {
                return NotFound();
            }

            return View(classSession);
        }

        // GET: AdminClassSessions/Create
        public IActionResult Create()
        {
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "LastName");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name");
            return View();
        }

        // POST: AdminClassSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassSessionId,Title,Description,StartTime,EndTime,RoomId,InstructorId")] ClassSession classSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "InstructorId", classSession.InstructorId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", classSession.RoomId);
            return View(classSession);
        }

        // GET: AdminClassSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession == null)
            {
                return NotFound();
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "LastName", classSession.InstructorId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "Name", classSession.RoomId);
            return View(classSession);
        }

        // POST: AdminClassSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassSessionId,Title,Description,StartTime,EndTime,RoomId,InstructorId")] ClassSession classSession)
        {
            if (id != classSession.ClassSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassSessionExists(classSession.ClassSessionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "InstructorId", "InstructorId", classSession.InstructorId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", classSession.RoomId);
            return View(classSession);
        }

        // GET: AdminClassSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions
                .Include(c => c.Instructor)
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.ClassSessionId == id);
            if (classSession == null)
            {
                return NotFound();
            }

            return View(classSession);
        }

        // POST: AdminClassSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession != null)
            {
                _context.ClassSessions.Remove(classSession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassSessionExists(int id)
        {
            return _context.ClassSessions.Any(e => e.ClassSessionId == id);
        }
    }
}
