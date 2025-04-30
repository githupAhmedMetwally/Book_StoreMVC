using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repositiries
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        IBookRepository Book { get; }
        IAuthorRepository Author { get; }
        IPublisherRepository Publisher { get; }
        ILoansRepository Loans { get; }
        int Complete();
    }
}
