using DataAccess.Impelementaion;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Models.Repositiries;
using Models.ViewModels;

namespace Book_Mangement_System.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var books = unitOfWork.Book.GetAll(Includeword:"Category,Author,Publisher");
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM()
            {
                Book=new Book(),
                CategoryList = unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                PublisherList = unitOfWork.Publisher.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                AuthorList = unitOfWork.Author.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),


            };
            return View(bookVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookVM bookVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var rootPath = _webHostEnvironment.WebRootPath; //wwwroot folder
                if (file != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(rootPath, @"Images\Book");
                    var ext = Path.GetExtension(file.FileName);
                    using (var filestream = new FileStream(Path.Combine(Upload, fileName + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    bookVM.Book.Img = @"Images\Book\" + fileName + ext;
                }
                unitOfWork.Book.Add(bookVM.Book);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(bookVM.Book);
        }

        [HttpGet]
        public IActionResult Edit(int ? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            BookVM bookVM = new BookVM()
            {
                Book = unitOfWork.Book.GetFirstorDefault(x=>x.Id==id),
                CategoryList = unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                PublisherList = unitOfWork.Publisher.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                AuthorList = unitOfWork.Author.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),


            };
            return View(bookVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookVM bookVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var rootPath = _webHostEnvironment.WebRootPath; //wwwroot folder
                if (file != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var Upload = Path.Combine(rootPath, @"Images\Book");
                    var ext = Path.GetExtension(file.FileName);
                    if (bookVM.Book.Img != null)
                    {
                        var oldimg = Path.Combine(rootPath, bookVM.Book.Img.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimg))
                        {
                            System.IO.File.Delete(oldimg);
                        }
                    }
                    using (var filestream = new FileStream(Path.Combine(Upload, fileName + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    bookVM.Book.Img = @"Images\Book\" + fileName + ext;
                }
                unitOfWork.Book.Update(bookVM.Book);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(bookVM.Book);
        }

        public IActionResult Delete(int ?id)
        {
            var bookIndb = unitOfWork.Book.GetFirstorDefault(x => x.Id == id);
            if (bookIndb == null)
            {
                return NotFound();
            }
            unitOfWork.Book.Remove(bookIndb);
            var oldimg = Path.Combine(_webHostEnvironment.WebRootPath, bookIndb.Img.TrimStart('\\'));
            if (System.IO.File.Exists(oldimg))
            {
                System.IO.File.Delete(oldimg);
            }
            unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
