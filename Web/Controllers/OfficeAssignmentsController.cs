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
    public class OfficeAssignmentsController : Controller
    {
        private DbSchoolContext db = new DbSchoolContext();

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        // GET: OfficeAssignments
        public async Task<ActionResult> List()
        {
            var officeAssignments = db.OfficeAssignments.Include(o => o.Person);
            return View(await officeAssignments.ToListAsync());
        }

        // GET: OfficeAssignments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = await db.OfficeAssignments.FindAsync(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName");
            return View();
        }

        // POST: OfficeAssignments/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InstructorID,Location,Timestamp")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                db.OfficeAssignments.Add(officeAssignment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = await db.OfficeAssignments.FindAsync(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // POST: OfficeAssignments/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "InstructorID,Location,Timestamp")] OfficeAssignment officeAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(officeAssignment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorID = new SelectList(db.People, "PersonID", "LastName", officeAssignment.InstructorID);
            return View(officeAssignment);
        }

        // GET: OfficeAssignments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OfficeAssignment officeAssignment = await db.OfficeAssignments.FindAsync(id);
            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            return View(officeAssignment);
        }

        // POST: OfficeAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OfficeAssignment officeAssignment = await db.OfficeAssignments.FindAsync(id);
            db.OfficeAssignments.Remove(officeAssignment);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
