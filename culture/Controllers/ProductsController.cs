using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using culture.Data;
using culture.Models;

namespace culture.Controllers
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using culture.Data;
    using System.IO;
    using System.Threading.Tasks;

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AzureBlobStorageService _blobService;

        public ProductsController(ApplicationDbContext context, AzureBlobStorageService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.FarmerProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.FarmerProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var farmerEmail = User.Identity.Name;
            var farmerProfile = _context.Farmers.FirstOrDefault(f => f.Email == farmerEmail);
            var viewModel = new Models.ProductWithFarmerProfileViewModel();

            if (farmerProfile != null)
            {
                viewModel.Product = new Models.Product { FarmerProfileId = farmerProfile.Id };
            }
            else
            {
                // Handle case when farmerProfile is null, e.g., redirect or error
                return RedirectToAction("Create", "FarmerProfiles");
            }
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.ProductWithFarmerProfileViewModel viewModel, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var farmerEmail = User.Identity.Name;
                var farmerProfile = _context.Farmers.FirstOrDefault(f => f.Email == farmerEmail);

                if (farmerProfile == null)
                {
                    // FarmerProfile must exist to create product
                    ModelState.AddModelError("", "Farmer profile not found. Please create a farmer profile first.");
                    return View(viewModel);
                }

                var product = viewModel.Product;
                if (product == null)
                {
                    product = new Models.Product();
                }
                product.FarmerProfileId = farmerProfile.Id;

                if (imageFile != null && imageFile.Length > 0)
                {
                    using var stream = imageFile.OpenReadStream();
                    var fileName = Path.GetFileName(imageFile.FileName);
                    product.ImageUrl = await _blobService.UploadFileAsync(stream, fileName);
                }
                else
                {
                    // Set default placeholder image URL if no image uploaded
                    product.ImageUrl = "/images/default-product-image.jpg";
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product created successfully.";
                return RedirectToAction("Index", "FarmerDashboard");
            }
            // Repopulate emails for dropdown on validation failure
            var emails = _context.Farmers.Select(f => new { Value = f.Email, Text = f.Email }).ToList();
            ViewData["FarmerEmails"] = new SelectList(emails, "Value", "Text");
            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["FarmerProfileId"] = new SelectList(_context.Farmers, "Id", "Email", product.FarmerProfileId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Category,ProductionDate,ImageUrl,FarmerProfileId")] Product product, IFormFile imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Assign FarmerProfileId based on logged-in user's email
                    var farmerEmail = User.Identity.Name;
                    var farmerProfile = _context.Farmers.FirstOrDefault(f => f.Email == farmerEmail);
                    if (farmerProfile == null)
                    {
                        ModelState.AddModelError("", "Farmer profile not found.");
                        return View(product);
                    }
                    product.FarmerProfileId = farmerProfile.Id;

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        using var stream = imageFile.OpenReadStream();
                        var fileName = Path.GetFileName(imageFile.FileName);
                        product.ImageUrl = await _blobService.UploadFileAsync(stream, fileName);
                    }
                    else if (string.IsNullOrEmpty(product.ImageUrl))
                    {
                        product.ImageUrl = "/images/default-product-image.jpg";
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FarmerProfileId"] = new SelectList(_context.Farmers, "Id", "Email", product.FarmerProfileId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.FarmerProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
