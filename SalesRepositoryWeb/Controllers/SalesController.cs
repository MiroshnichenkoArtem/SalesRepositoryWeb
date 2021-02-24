using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesRepositoryWeb.DAL;
using SalesRepositoryWeb.Models;

namespace SalesRepositoryWeb.Controllers
{
    public class SalesController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.DbSet.Include(s => s.Customer).Include(s => s.Manager).Include(s => s.Product);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.DbSet.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers.DbSet, "Id", "Lastname");
            ViewBag.ManagerId = new SelectList(db.Managers.DbSet, "Id", "Lastname");
            ViewBag.ProductId = new SelectList(db.Products.DbSet, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Price,ManagerId,CustomerId,ProductId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Create(sale);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers.DbSet, "Id", "Lastname", sale.CustomerId);
            ViewBag.ManagerId = new SelectList(db.Managers.DbSet, "Id", "Lastname", sale.ManagerId);
            ViewBag.ProductId = new SelectList(db.Products.DbSet, "Id", "Name", sale.ProductId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.DbSet.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers.DbSet, "Id", "Lastname", sale.CustomerId);
            ViewBag.ManagerId = new SelectList(db.Managers.DbSet, "Id", "Lastname", sale.ManagerId);
            ViewBag.ProductId = new SelectList(db.Products.DbSet, "Id", "Name", sale.ProductId);
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Price,ManagerId,CustomerId,ProductId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Update(sale);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers.DbSet, "Id", "Lastname", sale.CustomerId);
            ViewBag.ManagerId = new SelectList(db.Managers.DbSet, "Id", "Lastname", sale.ManagerId);
            ViewBag.ProductId = new SelectList(db.Products.DbSet, "Id", "Name", sale.ProductId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.DbSet.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.DbSet.Find(id);
            db.Sales.Remove(sale);
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
