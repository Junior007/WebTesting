using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolDataBase;

namespace Web.Controllers
{
    public class CoursesController : Controller
    {
        private DbSchoolContext db = new DbSchoolContext();

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        // GET: Courses
        public async Task<ActionResult> List()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.OnlineCourse).Include(c => c.OnsiteCourse);
            return View(await courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.CourseID = new SelectList(db.OnlineCourses, "CourseID", "URL");
            ViewBag.CourseID = new SelectList(db.OnsiteCourses, "CourseID", "Location");
            return View();
        }

        // POST: Courses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            ViewBag.CourseID = new SelectList(db.OnlineCourses, "CourseID", "URL", course.CourseID);
            ViewBag.CourseID = new SelectList(db.OnsiteCourses, "CourseID", "Location", course.CourseID);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            ViewBag.CourseID = new SelectList(db.OnlineCourses, "CourseID", "URL", course.CourseID);
            ViewBag.CourseID = new SelectList(db.OnsiteCourses, "CourseID", "Location", course.CourseID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
            ViewBag.CourseID = new SelectList(db.OnlineCourses, "CourseID", "URL", course.CourseID);
            ViewBag.CourseID = new SelectList(db.OnsiteCourses, "CourseID", "Location", course.CourseID);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course course = await db.Courses.FindAsync(id);
            db.Courses.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public ActionResult InstructorsPerCourse(int id)
        {
            /*TODO: Primera prueba
             * Esto sería parte del negocio
             * Abro el conexto de datos para obtener un listado
            */
            var repository = new SchoolDataBase.Repository();

            var course = repository.getCourse(id);
            return View(course);
        }
        public ActionResult StudentGradesPerCourse(int id)
        {
            var repository = new SchoolDataBase.Repository();

            var course = repository.getCourse(id);
            return View(course);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
