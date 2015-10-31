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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "address";

            if (searchString != null)
            {
                page = 1;  
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var donors = from d in db.Donors
                              select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                donors = donors.Where(d => d.LastName.ToUpper().Contains(searchString.ToUpper())
                    || d.HouseNumber.ToUpper().Contains(searchString.ToUpper())
                    || d.Street1.ToUpper().Contains(searchString.ToUpper())
                    || d.Telephone.ToUpper().Contains(searchString.ToUpper())
                    || d.AltPhone.ToUpper().Contains(searchString.ToUpper())
                    || d.Email.ToUpper().Contains(searchString.ToUpper())
                    );        
            }      
 
            switch (sortOrder)
            {
                case "name_desc":
                    donors = donors.OrderByDescending(d => d.LastName);
                    break;
                case "address":
                    donors = donors.OrderBy(d => d.Street1);
                    break;
                case "address_desc":
                    donors = donors.OrderByDescending(d => d.Street1);
                    break;
                default:
                    donors = donors.OrderBy(d => d.LastName);
                    break;
            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);

             return View(donors.ToPagedList(pageNumber, pageSize));
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
