using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Repositiries;
using System;

namespace Book_Mangement_System.web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var books = unitOfWork.Book.GetAll(Includeword: "Category,Author,Publisher");
            return View(books);
        }
        public async Task<IActionResult> Details(int ?id)
        {
          
            if(id==null || id == 0)
            {
                return NotFound();
            }

           var oldId = unitOfWork.Book.GetFirstorDefault(x => x.Id == id, Includeword: "Category,Author,Publisher");
            var user = await _userManager.GetUserAsync(User);

            bool alreadyBorrowed = false;

            if (user != null)
            {
                var existingLoan = unitOfWork.Loans.GetAll(x =>
                    x.UserId == user.Id && x.BookId == oldId.Id && !x.IsReturned).FirstOrDefault();

                alreadyBorrowed = existingLoan != null;
            }

            ViewBag.AlreadyBorrowed = alreadyBorrowed;
            return View(oldId);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        
        public async Task<IActionResult> Details(Book book)
        {
            // جلب الكتاب من قاعدة البيانات
            var boook = unitOfWork.Book.GetFirstorDefault(x => x.Id == book.Id);

            // تأكد من أن الكتاب موجود
            if (boook == null)
            {
               
                return RedirectToAction("Details", new { id = book.Id });
            }

            // تأكد أن الكتاب به نسخ متاحة للاستعارة
            if (boook.NumberOfCopies <= 0)
            {
                
                return RedirectToAction("Details", new { id = book.Id });
            }

            
            var user = await _userManager.GetUserAsync(User);

            // تحقق إذا كان المستخدم قد استعاره الكتاب من قبل
            var existingLoan = unitOfWork.Loans.GetAll(x => x.UserId == user.Id && x.BookId == book.Id && !x.IsReturned).FirstOrDefault();

            if (existingLoan != null)
            {
                
                return RedirectToAction("Details", new { id = book.Id });
            }

            // إنشاء قيد استعارَة جديد
            var bookLoan = new BookLoans
            {
                BookId = book.Id,
                UserId = user.Id,
                LoanDate = DateTime.Now,
                IsReturned = false
            };

            // إضافة قيد الاستعارة إلى الـ Loans
            unitOfWork.Loans.Add(bookLoan);
            boook.NumberOfCopies--;

            unitOfWork.Complete();

            

            return RedirectToAction("Details", new { id = book.Id });
        }

    }
}

 
    
