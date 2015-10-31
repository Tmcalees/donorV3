using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastReport;
using FastReport.Web;
using FastReport.Utils;

namespace VOA.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult AddressList()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/AddressLists.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult TruckSlips()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/TruckSlips.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult CustomerReceipts()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/CustomerReceipts.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult PickupsByOperator()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/PickupsByOperator.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult PickupsByHour()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/PickupsByHour.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult PickupsByZip()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/PickupsByZipCode.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult DropOffsByOperator()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/DropOffsByOperator.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult DropOffsByStore()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/DropOffsByStore.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }

        // GET: Report
        public ActionResult OtherMerchandise()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/OtherMerchandise.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("ReportViewer");
        }
    }
}