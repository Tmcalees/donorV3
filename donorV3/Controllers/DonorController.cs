using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace VOA.Models
{
    public class DonorController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        // GET: /Donor/
        public ActionResult Index(string lastName, string firstName, string zipCode, int? page)
        {            
               return Search(lastName, firstName, zipCode, page);
        }

        // GET: /Donor/Search
        public ActionResult Search(string lastName, string firstName, string zipCode, int? page)
        {
            ViewBag.SearchLastName = lastName;
            ViewBag.SearchFirstName = firstName;
            ViewBag.SearchZipCode = zipCode;

            var donors = from d in db.Donors
                         select d;

            if (!String.IsNullOrEmpty(lastName))
            {
                donors = donors.Where(d => d.LastName.ToUpper().Contains(lastName.ToUpper()));
            }

            if (!String.IsNullOrEmpty(firstName))
            {
                donors = donors.Where(d => d.FirstName.ToUpper().Contains(firstName.ToUpper()));
            }

            if (!String.IsNullOrEmpty(zipCode))
            {
                donors = donors.Where(d => d.ZipCode.ToUpper().Contains(zipCode.ToUpper()));
            }

            donors = donors.OrderBy(d => d.LastName);

            int pageSize = 25;
            int pageNumber = (page ?? 1);

            return View("Index", donors.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Donor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // GET: /Donor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Donor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,LastName,FirstName,HouseNumber,Street1,Street2,City,State,ZipCode,Telephone,Notes,AltPhone,Email")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Donors.Add(donor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donor);
        }

        // GET: /Donor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: /Donor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,LastName,FirstName,HouseNumber,Street1,Street2,City,State,ZipCode,Telephone,Notes,AltPhone,Email")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donor);
        }

        // GET: /Donor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donors.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        // POST: /Donor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donor donor = db.Donors.Find(id);
            db.Donors.Remove(donor);
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
