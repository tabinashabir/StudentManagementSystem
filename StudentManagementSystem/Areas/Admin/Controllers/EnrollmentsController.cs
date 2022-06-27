using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnrollmentsController : Controller
    {
        private readonly StudentManagementSystemDbContext _context;

        public EnrollmentsController(StudentManagementSystemDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var studentManagementSystemDbContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await studentManagementSystemDbContext.ToListAsync());
        }

        // GET: Enrollments/Details
        public IActionResult Details(int id)
        {
            var enrollment = _context.Enrollments.Find(id);

            if (enrollment == null)
            {
                Response.StatusCode = 404;
                return View("PageNotFound", id);
            }

            var objEnrollmentList = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Where(m => m.EnrollmentID == id);
            return View(objEnrollmentList);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title");
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName");
            return View();
        }

        // POST: Enrollments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID")] Enrollment enrollment)
        {
            foreach (var item in _context.Enrollments)
            {
                if (item.CourseID == enrollment.CourseID && item.StudentID == enrollment.StudentID)
                {
                    ModelState.AddModelError("CourseID", "The student is already enrolled in this course");
                    return View(enrollment);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                TempData["success"] = "Enrollment created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentID,CourseID,StudentID")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Enrollment updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
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
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(_context.Students, "ID", "FirstName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'StudentManagementSystemDbContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "Enrollment deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return (_context.Enrollments?.Any(e => e.EnrollmentID == id)).GetValueOrDefault();
        }
    }
}
