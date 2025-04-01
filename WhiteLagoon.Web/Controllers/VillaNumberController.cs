using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entites;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villaNumbers = _context.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumber = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })

            };
            return View(villaNumber);
        }

        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            ModelState.Remove("Villa");
            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "The villa Number has been created  successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Update(int VillaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(x => x.Id == VillaId);
            if (obj == null)
            {    
                return RedirectToAction("Error","Home");
            }
            return View(obj);
         }
        
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id>0)
            {
                _context.Villas.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "The villa has been Updated successfully.";
                return RedirectToAction("Index");
            }
            return View();  
        }

        public IActionResult Delete(int VillaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(x => x.Id == VillaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _context.Villas.FirstOrDefault(x =>x.Id == obj.Id);  
            if (objFromDb is not null)
            {
                _context.Villas.Remove(objFromDb);
                _context.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted.";
            return View();
        }

    }
}
