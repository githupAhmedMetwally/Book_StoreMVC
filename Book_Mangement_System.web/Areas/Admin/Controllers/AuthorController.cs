using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Repositiries;

namespace Book_Mangement_System.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Authores = unitOfWork.Author.GetAll(Includeword: "Books");
            return View(Authores);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Author.Add(author);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(author);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AuId = unitOfWork.Author.GetFirstorDefault(x => x.Id == id);
            return View(AuId);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Author.Update(author);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(author);
        }
        public IActionResult Delete(int? id)
        {
            var Aid = unitOfWork.Author.GetFirstorDefault(x => x.Id == id);
            if (Aid == null)
            {
                return NotFound();
            }
            unitOfWork.Author.Remove(Aid);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
