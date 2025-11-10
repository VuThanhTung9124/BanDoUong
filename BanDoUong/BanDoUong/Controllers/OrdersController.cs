using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanDoUong.Models;
using Newtonsoft.Json;

namespace BanDoUong.Controllers
{
    public class OrdersController : Controller
    {
        private DonHangDb db = new DonHangDb();

        // GET: Orders
        [HttpGet]
        public ActionResult Index()
        {
            // Chỉ lấy hóa đơn mới nhất
            var latestOrder = db.Orders
                                .OrderByDescending(o => o.OrderId)
                                .FirstOrDefault();
            return View(latestOrder);
        }

        [HttpPost]
        public ActionResult Index(string FullName, string Phone, string Address)
        {

            Order order = new Order();
            order.FullName = FullName;
            order.Phone = Phone;
            order.Address = Address;
            order.TotalAmount = Convert.ToDecimal( Session["tong_tien"]);
            order.Description = Session["thong_tin"].ToString();
            
           
            db.Orders.Add(order);
            db.SaveChanges();

            // Chỉ lấy hóa đơn mới nhất
            var latestOrder = db.Orders
                                .OrderByDescending(o => o.OrderId)
                                .FirstOrDefault();

            return View(latestOrder);
        }


        [HttpPost]
        public ActionResult MuaMotSanPham(string FullName, string Phone, string Address)
        {

            Order order = new Order();
            order.FullName = FullName;
            order.Phone = Phone;
            order.Address = Address;
            order.TotalAmount = Convert.ToDecimal(Session["tong_tien"]);
            order.Description = Session["thong_tin"].ToString();


            db.Orders.Add(order);
            db.SaveChanges();

            // Chỉ lấy hóa đơn mới nhất
            var latestOrder = db.Orders
                                .OrderByDescending(o => o.OrderId)
                                .FirstOrDefault();

            return View(latestOrder);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,FullName,Phone,Address,TotalAmount,Description")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,FullName,Phone,Address,TotalAmount,Description")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
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
