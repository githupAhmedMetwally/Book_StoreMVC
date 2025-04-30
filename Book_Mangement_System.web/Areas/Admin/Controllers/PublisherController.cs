using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Repositiries;

namespace Book_Mangement_System.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PublisherController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var publishers = unitOfWork.Publisher.GetAll(Includeword:"Books");
            return View(publishers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Publisher.Add(publisher);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }
        [HttpGet]
        public IActionResult Edit(int ? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            var puId = unitOfWork.Publisher.GetFirstorDefault(x => x.Id == id);
            return View(puId);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Publisher.Update(publisher);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }
        public IActionResult Delete(int ? id)
        {
            var Pid = unitOfWork.Publisher.GetFirstorDefault(x => x.Id == id);
            if (Pid == null)
            {
                return NotFound();
            }
            unitOfWork.Publisher.Remove(Pid);
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
