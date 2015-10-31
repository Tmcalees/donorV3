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
    public class ConfigDataController : Controller
    {
        private ConfigDataDBContext db = new ConfigDataDBContext();

        // GET: /ConfigData/
        public ActionResult Index()
        {
            return View(db.ConfigData.ToList());
        }

        // GET: /ConfigData/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigData configdata = db.ConfigData.Find(id);
            if (configdata == null)
            {
                return HttpNotFound();
            }
            return View(configdata);
        }

        // GET: /ConfigData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ConfigData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Value")] ConfigData configdata)
        {
            if (ModelState.IsValid)
            {
                db.ConfigData.Add(configdata);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configdata);
        }

        // GET: /ConfigData/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigData configdata = db.ConfigData.Find(id);
            if (configdata == null)
            {
                return HttpNotFound();
            }
            return View(configdata);
        }

        // POST: /ConfigData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Value")] ConfigData configdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configdata);
        }

        // GET: /ConfigData/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConfigData configdata = db.ConfigData.Find(id);
            if (configdata == null)
            {
                return HttpNotFound();
            }
            return View(configdata);
        }

        // POST: /ConfigData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ConfigData configdata = db.ConfigData.Find(id);
            db.ConfigData.Remove(configdata);
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
