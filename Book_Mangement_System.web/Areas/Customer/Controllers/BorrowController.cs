using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Repositiries;

namespace Book_Mangement_System.web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BorrowController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;

        public BorrowController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task< IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
             
            var AllBorrros = unitOfWork.Loans.GetAll(x => x.UserId == user.Id &&!x.IsReturned ,Includeword:"Book");

             
            return View(AllBorrros);
        }
        public IActionResult Return(int? id)
        {
            
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var loan = unitOfWork.Loans.GetFirstorDefault(x => x.Id == id,Includeword:"Book");
            if(loan!=null && !loan.IsReturned)
            {
                loan.IsReturned = true;
                loan.Book.NumberOfCopies++;
                unitOfWork.Complete();
                return RedirectToAction("Index","Home");
            }
            return View("Index");
        }
    }
}
