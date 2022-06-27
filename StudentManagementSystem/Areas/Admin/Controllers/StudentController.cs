using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly StudentManagementSystemDbContext _db;

        public StudentController(StudentManagementSystemDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> objStudentList = _db.Students;
            return View(objStudentList);
        }

        // GET: Students/Details
        public IActionResult Details(int id)
        {
            var student = _db.Students.Find(id);

            if (student == null)
            {
                Response.StatusCode = 404;
                return View("PageNotFound", id);
            }

            var objStudent = _db.Students
                .Include(x => x.Enrollments)
                .FirstOrDefault(x => x.ID == id);
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
        public IActionResult Create(Student obj)
        {
            if (_db.Students.Any(x => x.EmailAddress == obj.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "The student with same email already exists");
                return View(obj);
            }
            if (_db.Students.Any(x => x.PhoneNumber == obj.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "The student with same phone no. already exists");
                return View(obj);
            }
            if (ModelState.IsValid)
            {
                _db.Students.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Student created successfully";
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
            var categoryFromDb = _db.Students.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student obj)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Student updated successfully";
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
            var categoryFromDb = _db.Students.Find(id);

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
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Students.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Student deleted successfully";
            return RedirectToAction("Index");

        }


    }
}
