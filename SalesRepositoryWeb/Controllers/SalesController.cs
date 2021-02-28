using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SalesRepositoryWeb.DAL;
using SalesRepositoryWeb.Models;
using PagedList;

namespace SalesRepositoryWeb.Controllers
{
    public class SalesController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: Sales
        public ActionResult  Index(int? page)
        {
            OrderFilter filter = (OrderFilter)Session["orderFilter"];
            ViewBag.CurrentPage = page ?? 1;
            if (filter != null)
            {
                return View(filter);
            }
            return View(new OrderFilter());
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

        [HttpPost]
        public ActionResult SalesSearch(string searchString)
        {
            var foundSales = db.Sales.DbSet.Where(x =>
                x.Customer.Lastname.Contains(searchString) || x.Manager.Lastname.Contains(searchString)).ToList();
            if (foundSales.Count == 0)
            {
                return HttpNotFound();
            }

            return PartialView(foundSales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Sales(int? page)
        {
            try
            {
                var sales = db.Sales.DbSet.ToList();
                OrderFilter filter = (OrderFilter)Session["orderFilter"];
                if (filter != null)
                {
                    sales = GetFilteredSales(sales, filter);
                }
                ViewBag.CurrentPage = page;
                return PartialView(sales.ToPagedList(page ?? 1, 4));
            }
            catch (Exception ex)
            {
                
                return View("Error");
            }
        }
        private List<Sale> GetFilteredSales(List<Sale> sales, OrderFilter filter)
        {
           
            if (filter.ProductName != null)
            {
                sales = sales.Where(x => x.Product.Name.Contains(filter.ProductName)).ToList();
            }
            if (filter.CustomerLastname != null)
            {
                sales = sales.Where(x => x.Customer.Lastname.Contains(filter.CustomerLastname)).ToList();
            }
            return sales;
        }
        [HttpPost]
        public ActionResult ApplyFilter(OrderFilter model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Sales", null);
            }
            Session["orderFilter"] = model;
            return RedirectToAction("Sales");
        }
        [HttpGet]
        public ActionResult ClearFilter()
        {
            Session["orderFilter"] = null;
            return RedirectToAction("Sales");
        }
    }
}
