using DataAccess.Data;
using Models.Repositiries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Impelementaion
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }
        public IAuthorRepository Author { get; private set; }
        public IPublisherRepository Publisher { get; private set; }
        public ILoansRepository Loans { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Book = new BookRepository(context);
            Author = new AuthorRepository(context);
            Publisher = new PublisherRepository(context);
            Loans = new LoansRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
