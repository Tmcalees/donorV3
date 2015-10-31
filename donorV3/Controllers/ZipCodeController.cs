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
    public class ZipCodeController : Controller
    {
        private ZipCodeDBContext db = new ZipCodeDBContext();

        // GET: /ZipCode/
        public ActionResult Index(string zipCode, string town)
        {
            var zipCodes = from z in db.ZipCodes
                              select z;

            if (!String.IsNullOrEmpty(zipCode))     // Append zip code where if search criteria provided
            {
                zipCodes = zipCodes.Where(z => z.Code.ToUpper().Contains(zipCode.ToUpper()));
            }

            if (!String.IsNullOrEmpty(town))        // Append town where if search criteria provided
            {
                zipCodes = zipCodes.Where(z => z.Town.ToUpper().Contains(town.ToUpper()));
            }
            string sql = zipCodes.ToString();
            Console.Out.WriteLine(sql);
            return View(db.ZipCodes.ToList());
        }

        // GET: /ZipCode/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipcode = db.ZipCodes.Find(id);
            if (zipcode == null)
            {
                return HttpNotFound();
            }
            return View(zipcode);
        }

        // GET: /ZipCode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ZipCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Code,Town,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,WeekOfMonth")] ZipCode zipcode)
        {
            if (ModelState.IsValid)
            {
                db.ZipCodes.Add(zipcode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zipcode);
        }

        // GET: /ZipCode/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipcode = db.ZipCodes.Find(id);
            if (zipcode == null)
            {
                return HttpNotFound();
            }
            return View(zipcode);
        }

        // POST: /ZipCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Code,Town,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday,WeekOfMonth")] ZipCode zipcode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zipcode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zipcode);
        }

        // GET: /ZipCode/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipcode = db.ZipCodes.Find(id);
            if (zipcode == null)
            {
                return HttpNotFound();
            }
            return View(zipcode);
        }

        // POST: /ZipCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZipCode zipcode = db.ZipCodes.Find(id);
            db.ZipCodes.Remove(zipcode);
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
