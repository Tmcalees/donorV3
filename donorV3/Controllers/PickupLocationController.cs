using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VOA.Models;

namespace VOA.Controllers
{
    public class PickupLocationController : Controller
    {
        private PickupLocationDBContext db = new PickupLocationDBContext();

        // GET: /PickupLocation/
        public ActionResult Index()
        {
            return View(db.PickupLocations.ToList());
        }

        // GET: /PickupLocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupLocation pickuplocation = db.PickupLocations.Find(id);
            if (pickuplocation == null)
            {
                return HttpNotFound();
            }
            return View(pickuplocation);
        }

        // GET: /PickupLocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PickupLocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Description,DisplayOrder,Active")] PickupLocation pickuplocation)
        {
            if (ModelState.IsValid)
            {
                db.PickupLocations.Add(pickuplocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pickuplocation);
        }

        // GET: /PickupLocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupLocation pickuplocation = db.PickupLocations.Find(id);
            if (pickuplocation == null)
            {
                return HttpNotFound();
            }
            return View(pickuplocation);
        }

        // POST: /PickupLocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Description,DisplayOrder,Active")] PickupLocation pickuplocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickuplocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pickuplocation);
        }

        // GET: /PickupLocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickupLocation pickuplocation = db.PickupLocations.Find(id);
            if (pickuplocation == null)
            {
                return HttpNotFound();
            }
            return View(pickuplocation);
        }

        // POST: /PickupLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickupLocation pickuplocation = db.PickupLocations.Find(id);
            db.PickupLocations.Remove(pickuplocation);
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
