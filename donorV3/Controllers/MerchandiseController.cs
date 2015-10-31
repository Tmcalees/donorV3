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
    public class MerchandiseController : Controller
    {
        private MerchandiseDBContext db = new MerchandiseDBContext();

        // GET: /Merchandise/
        public ActionResult Index(string searchString, bool showDisabled = false)
        {
            var merchandise = from m in db.Merchandise
                              select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                merchandise = merchandise.Where(m => m.Name.ToUpper().Contains(searchString.ToUpper()));

            }
            return View(merchandise.ToList());
        }

        // GET: /Merchandise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchandise merchandise = db.Merchandise.Find(id);
            if (merchandise == null)
            {
                return HttpNotFound();
            }
            return View(merchandise);
        }

        // GET: /Merchandise/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Merchandise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,CheckBox,Active")] Merchandise merchandise)
        {
            if (ModelState.IsValid)
            {
                db.Merchandise.Add(merchandise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(merchandise);
        }

        // GET: /Merchandise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchandise merchandise = db.Merchandise.Find(id);
            if (merchandise == null)
            {
                return HttpNotFound();
            }
            return View(merchandise);
        }

        // POST: /Merchandise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,CheckBox,Active")] Merchandise merchandise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(merchandise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(merchandise);
        }

        // GET: /Merchandise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Merchandise merchandise = db.Merchandise.Find(id);
            if (merchandise == null)
            {
                return HttpNotFound();
            }
            return View(merchandise);
        }

        // POST: /Merchandise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Merchandise merchandise = db.Merchandise.Find(id);
            db.Merchandise.Remove(merchandise);
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
