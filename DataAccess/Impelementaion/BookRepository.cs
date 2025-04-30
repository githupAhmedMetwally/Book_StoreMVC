using DataAccess.Data;
using Models.Models;
using Models.Repositiries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Impelementaion
{
    public  class BookRepository:GenericRepository<Book>,IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Book book)
        {
            var bookInDb = _context.Books.FirstOrDefault(x => x.Id == book.Id);
            if (bookInDb != null)
            {
                bookInDb.Title = book.Title;
                bookInDb.PublishDate = book.PublishDate;
                bookInDb.PublisherId = book.PublisherId;
                bookInDb.AuthorId = book.AuthorId;
                bookInDb.CategoryId = book.CategoryId;
                bookInDb.ISBN = book.ISBN;
                bookInDb.Img = book.Img;
                bookInDb.Pages = book.Pages;
                bookInDb.NumberOfCopies= book.NumberOfCopies;
            }
        }

         
    }
}
