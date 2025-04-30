using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Repositiries;

namespace Book_Mangement_System.web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categories = unitOfWork.Category.GetAll();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
          
            if(id == null || id==0)
            {
                return NotFound();
            }
            var Cid = unitOfWork.Category.GetFirstorDefault(x => x.Id == id);

            return View(Cid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Complete();
                return RedirectToAction("Index");

            }
            return View(category);
        }

        public IActionResult Delete(int ?id)
        { 
            var Cid = unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            if (Cid == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Remove(Cid);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
