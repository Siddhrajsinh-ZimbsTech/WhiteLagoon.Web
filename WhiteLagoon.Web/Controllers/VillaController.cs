using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entites;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Villas = _unitOfWork.Villa.GetAll();
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
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.save();
                TempData["success"] = "The villa has been created  successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Update(int VillaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(x => x.Id == VillaId);
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
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.save();
                TempData["success"] = "The villa has been Updated successfully.";
                return RedirectToAction("Index");
            }
            return View();  
        }

        public IActionResult Delete(int VillaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(x => x.Id == VillaId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _unitOfWork.Villa.Get(x =>x.Id == obj.Id);  
            if (objFromDb is not null)
            {
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.save();
                TempData["success"] = "The villa has been deleted successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted.";
            return View();
        }

    }
}
