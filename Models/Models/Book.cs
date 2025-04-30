using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Models.Models
{
    public  class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [ValidateNever]
        public string Img { get; set; }
        public string ISBN { get; set; }
        public int NumberOfCopies { get; set; }
        public DateTime PublishDate { get; set; }
        public int Pages { get; set; }
        [DisplayName("AuthorName")]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        [DisplayName("CategoryName")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [DisplayName("PublisherName")]
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        [ValidateNever]
        public Author Author { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public Publisher Publisher { get; set; }
         
    }
}
