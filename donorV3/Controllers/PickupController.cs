using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VOA.Models;
using PagedList;

namespace VOA.Controllers
{
    public class PickupController : Controller
    {
        private DonorDBContext dDB = new DonorDBContext();
        private MerchandiseDBContext mDB = new MerchandiseDBContext();
        private PickupLocationDBContext pDB = new PickupLocationDBContext();

        //
        // GET: /Pickup/
        public ActionResult Index()
        {            
            return View();
        }

        // GET: /Pickup/Search
        public ActionResult SearchDonor(string lastName, string firstName, string zipCode, int? page)
        {
            ViewBag.SearchLastName = lastName;
            ViewBag.SearchFirstName = firstName;
            ViewBag.SearchZipCode = zipCode;

            var donors = from d in dDB.Donors
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
            
            return View("ListDonors", donors.ToPagedList(pageNumber, pageSize));            
        }

        public ActionResult AddDonor()
        {
            return View("");
        }

        public ActionResult SelectDate(string donorId)
        {
            if (String.IsNullOrEmpty(donorId))
            {
                Response.Redirect("/Pickup");
                return null;
            }
            else
            { 
                var donor = dDB.Donors.Find(Int32.Parse(donorId));

                VOA.Utility.PickupAvailability puAvailability = new Utility.PickupAvailability();
            
                List<DateTime> availableDates = puAvailability.GetAvailablePickupDates(donor.ZipCode);

                return View("ListDates",availableDates);
            }
        }

        public ActionResult SelectMerchandise()
        {
            var merchandise = from m in mDB.Merchandise
                         where m.Active==true
                         orderby m.Name
                         select m;

            return View("ListMerchandise", merchandise.ToList());
        }

        public ActionResult SelectPickupLocation()
        {
            var pickupLocations = from p in pDB.PickupLocations
                                  orderby p.DisplayOrder
                                  select p;

            return View("ListPickupLocations", pickupLocations.ToList());
        }

        public ActionResult Review()
        {
            return View("ReviewPickup");
        }
    }
}