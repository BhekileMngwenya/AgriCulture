using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using culture.Data;

namespace culture.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string farmerRegion, string productType, DateTime? startDate, DateTime? endDate, string keyword)
        {
            var employeeName = User.Identity.Name;
            ViewData["EmployeeName"] = employeeName;
            ViewData["CurrentDateTime"] = DateTime.Now.ToString("f");

            var farmersQuery = _context.Farmers.AsQueryable();
            var productsQuery = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(farmerRegion))
            {
                farmersQuery = farmersQuery.Where(f => f.Province == farmerRegion);
            }

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
                farmersQuery = farmersQuery.Where(f => f.FullName.Contains(keyword) || f.Email.Contains(keyword));
                productsQuery = productsQuery.Where(p => p.Name.Contains(keyword));
            }

            var totalFarmers = farmersQuery.Count();
            var totalProducts = productsQuery.Count();
            var recentFarmers = farmersQuery
                .OrderByDescending(f => f.Id)
                .Take(5)
                .ToList();

            var recentProducts = productsQuery
                .OrderByDescending(p => p.ProductionDate)
                .Take(5)
                .ToList();

            ViewData["TotalFarmers"] = totalFarmers;
            ViewData["TotalProducts"] = totalProducts;
            ViewData["RecentFarmers"] = recentFarmers;
            ViewData["RecentProducts"] = recentProducts;

            return View();
        }
    }
}
