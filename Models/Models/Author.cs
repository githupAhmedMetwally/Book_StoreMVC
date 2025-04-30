using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Author
    { 
            public int Id { get; set; }

            public string Name { get; set; }
            public string Bio { get; set; }
            public DateTime BirthDate { get; set; }
        [ValidateNever]
            public ICollection<Book> Books { get; set; }
        

    }
}
