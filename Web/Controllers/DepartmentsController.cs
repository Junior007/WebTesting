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
    public class DepartmentsController : Controller
    {
        //private DbSchoolContext db = new DbSchoolContext();
        private Repository repository = new SchoolDataBase.Repository();

        public DepartmentsController()
        {
            //repository = new SchoolDataBase.Repository();
        }
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        // GET: Departments
        public ActionResult List()
        {
            //var departments;// = 
            //db.Departments.Include(d => d.Administrator);
            //return View(await departments.ToListAsync());

            var departments = repository.getDepartments();
            return View(departments);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            Department department = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                department = repository.getDepartment((int)id);
                if (department == null)
                {
                    return HttpNotFound();
                }
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.AdministratorID = new SelectList(repository.getPeople(), "PersonID", "LastName");
            return View();
        }

        // POST: Departments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,AdministratorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                repository.addDepartment(department);
                repository.SaveChanges();
                /*db.Departments.Add(department);
                await db.SaveChangesAsync();*/
                return RedirectToAction("Index");
            }

            ViewBag.AdministratorID = new SelectList(repository.getPeople(), "PersonID", "LastName", department.AdministratorID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = repository.getDepartment((int)id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdministratorID = new SelectList(repository.getPeople(), "PersonID", "LastName", department.AdministratorID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,Name,Budget,StartDate,AdministratorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                /*db.Entry(department).State = EntityState.Modified;
                await db.SaveChangesAsync();*/
                repository.updDepartment(department);
                repository.SaveChanges();


                return RedirectToAction("Index");
            }
            ViewBag.AdministratorID = new SelectList(repository.getPeople(), "PersonID", "LastName", department.AdministratorID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = repository.getDepartment((int)id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*Department department = await db.Departments.FindAsync(id);
              db.Departments.Remove(department);
              await db.SaveChangesAsync();
              */

            repository.removeDepartament(id);
            repository.SaveChanges();
            return RedirectToAction("Index");
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
