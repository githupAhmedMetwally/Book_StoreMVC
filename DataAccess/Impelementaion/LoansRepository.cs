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
    public class LoansRepository : GenericRepository<BookLoans>, ILoansRepository
    {
        private readonly ApplicationDbContext context;

        public LoansRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
