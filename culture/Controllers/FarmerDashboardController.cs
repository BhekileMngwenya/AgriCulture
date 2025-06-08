using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using culture.Data;
using Microsoft.EntityFrameworkCore;

namespace culture.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string productType, DateTime? startDate, DateTime? endDate, string keyword)
        {
            var farmerName = User.Identity.Name;
            ViewData["FarmerName"] = farmerName;
            ViewData["CurrentDateTime"] = DateTime.Now.ToString("f");

            // Fetch farmer profile by email (assuming User.Identity.Name is email)
            var farmerProfile = _context.Farmers.FirstOrDefault(f => f.Email == farmerName);

            if (farmerProfile != null)
            {
                var productsQuery = _context.Products
                    .Where(p => p.FarmerProfileId == farmerProfile.Id);

                if (!string.IsNullOrEmpty(productType))
                {
                    productsQuery = productsQuery.Where(p => p.Category == productType);
                }

                if (startDate.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.ProductionDate <= endDate.Value);
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    productsQuery = productsQuery.Where(p => p.Name.Contains(keyword));
                }

                var products = productsQuery.OrderByDescending(p => p.ProductionDate).ToList();

                ViewData["TotalProducts"] = products.Count;
                if (products.Any())
                {
                    var lastProduct = products.First();
                    ViewData["LastProductName"] = lastProduct.Name;
                    ViewData["LastProductDate"] = lastProduct.ProductionDate.ToString("d");
                }
                else
                {
                    ViewData["LastProductName"] = "N/A";
                    ViewData["LastProductDate"] = "N/A";
                }
                ViewData["RecentProducts"] = products.Take(5).ToList();
            }
            else
            {
                ViewData["TotalProducts"] = 0;
                ViewData["LastProductName"] = "N/A";
                ViewData["LastProductDate"] = "N/A";
                ViewData["RecentProducts"] = new List<object>();
            }

            return View();
        }
    }
}
