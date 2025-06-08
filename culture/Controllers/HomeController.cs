using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace culture.Controllers
{
    public class HomeController : Controller
    {
        private static List<string> ReportedIssues = new List<string>();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ReportIssue()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportIssue(string issueDescription)
        {
            if (!string.IsNullOrWhiteSpace(issueDescription))
            {
                ReportedIssues.Add(issueDescription);
                ViewBag.Message = "Thank you for reporting the issue. We will look into it.";
            }
            else
            {
                ViewBag.Message = "Please enter a valid issue description.";
            }
            return View();
        }

        // Optional: To view reported issues (for testing)
        public IActionResult ViewReportedIssues()
        {
            return View(ReportedIssues);
        }
    }
}
