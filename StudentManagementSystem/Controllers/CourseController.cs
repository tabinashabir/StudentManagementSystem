using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly StudentManagementSystemDbContext _db;

        public CourseController(StudentManagementSystemDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Course> objCourseList = _db.Courses;
            return View(objCourseList);
        }

        // GET: Course/Details
        public IActionResult Details(int id)
        {

            var objStudent = _db.Courses
                .Include(x => x.Enrollments)
                .ThenInclude(y => y.Student)
                .FirstOrDefault((x => x.CourseID == id));
            return View(objStudent);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course obj)
        {
            if(_db.Courses.Any(x => x.Title == obj.Title))
            {
                ModelState.AddModelError("Title", "The course already exists");
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                _db.Courses.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Courses.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course obj)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Courses.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Courses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Courses.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Course deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
