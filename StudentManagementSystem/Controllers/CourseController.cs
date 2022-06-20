using Microsoft.AspNetCore.Mvc;
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
            //if (obj.Frist == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            _db.Courses.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Course created successfully";
            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    _db.Students.Add(obj);
            //    _db.SaveChanges();
            //    TempData["success"] = "Category created successfully";
            //    return RedirectToAction("Index");
            //}
            //return View(obj);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Courses.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

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
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            _db.Courses.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Course updated successfully";
            return RedirectToAction("Index");
            //if (ModelState.IsValid)
            //{
            //    _db.Students.Update(obj);
            //    _db.SaveChanges();
            //    TempData["success"] = "Category updated successfully";
            //    return RedirectToAction("Index");
            //}
            //return View(obj);
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
