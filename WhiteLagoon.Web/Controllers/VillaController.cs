using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entites;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Villas = _context.Villas.ToList();
            return View(Villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("Name", "The description cannot exactly match the Name. ");
            }
            if (ModelState.IsValid)
            {
                _context.Villas.Add(obj);
                _context.SaveChanges();
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
    }
}
