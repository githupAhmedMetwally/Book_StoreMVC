using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Repositiries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Impelementaion
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext context;

        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Author author)
        {
            var AuthorInDb = context.Authors.FirstOrDefault(x => x.Id == author.Id);
            if (AuthorInDb != null)
            {
                AuthorInDb.Name=author.Name;
                AuthorInDb.Bio=author.Bio;
                AuthorInDb.BirthDate=author.BirthDate;
            }
        }
    }
}
