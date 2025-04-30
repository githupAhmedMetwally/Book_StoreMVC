using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Publisher
    { 
            public int Id { get; set; }

            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
        [ValidateNever]
            public ICollection<Book> Books { get; set; }
        
    }
}
