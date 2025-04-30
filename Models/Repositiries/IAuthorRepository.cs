using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Repositiries
{
    public  interface IAuthorRepository:IGenericRepository<Author>
    {
        void Update(Author author);
    }
}
