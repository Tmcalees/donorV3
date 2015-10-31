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
    public class AnalyticsController : Controller
    {
        // GET: MerchandiseTrend
        public ActionResult MerchandiseTrend()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/MerchandiseTrend.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("AnalyticsViewer");
        }

        // GET: ZipSummary
        public ActionResult ZipSummary()
        {
            WebReport webReport = new WebReport();

            webReport.Width = 1024;          // set width
            webReport.Height = 800;         // set height            
            webReport.ReportFile = this.Server.MapPath("~/App_Data/ZipDonationSummary.frx");       // load the report from the file            
            ViewBag.WebReport = webReport;  // send object to the View

            return View("AnalyticsViewer");
        }
    }
}