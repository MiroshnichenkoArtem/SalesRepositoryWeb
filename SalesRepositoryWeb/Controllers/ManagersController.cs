using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SalesRepositoryWeb.DAL;
using SalesRepositoryWeb.Models;

namespace SalesRepositoryWeb.Controllers
{
    public class ManagersController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: Managers
        public ActionResult Index()
        {
            return View(db.Managers.DbSet.ToList());
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.DbSet.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Lastname")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Create(manager);
                return RedirectToAction("Index");
            }

            return View(manager);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.DbSet.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Lastname")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Update(manager);
                return RedirectToAction("Index");
            }
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.DbSet.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.DbSet.Find(id);
            db.Managers.Remove(manager);
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
