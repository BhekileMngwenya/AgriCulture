using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using culture.Data;
using culture.Models;

namespace culture.Controllers
{
    public class FarmerProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmerProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FarmerProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmers.ToListAsync());
        }

        // GET: FarmerProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerProfile = await _context.Farmers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmerProfile == null)
            {
                return NotFound();
            }

            return View(farmerProfile);
        }

        // GET: FarmerProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FarmerProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,DateOfBirth,Gender,PhoneNumber,Email,FarmName,StreetAddress,TownCity,Province,PostalCode,FarmSize,FarmingType,NumberOfWorkers,YearsOfExperience")] FarmerProfile farmerProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(farmerProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmerProfile);
        }

        // GET: FarmerProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerProfile = await _context.Farmers.FindAsync(id);
            if (farmerProfile == null)
            {
                return NotFound();
            }
            return View(farmerProfile);
        }

        // POST: FarmerProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,DateOfBirth,Gender,PhoneNumber,Email,FarmName,StreetAddress,TownCity,Province,PostalCode,FarmSize,FarmingType,NumberOfWorkers,YearsOfExperience")] FarmerProfile farmerProfile)
        {
            if (id != farmerProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farmerProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerProfileExists(farmerProfile.Id))
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
            return View(farmerProfile);
        }

        // GET: FarmerProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmerProfile = await _context.Farmers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmerProfile == null)
            {
                return NotFound();
            }

            return View(farmerProfile);
        }

        // POST: FarmerProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmerProfile = await _context.Farmers.FindAsync(id);
            if (farmerProfile != null)
            {
                _context.Farmers.Remove(farmerProfile);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FarmerProfileExists(int id)
        {
            return _context.Farmers.Any(e => e.Id == id);
        }
    }
}
