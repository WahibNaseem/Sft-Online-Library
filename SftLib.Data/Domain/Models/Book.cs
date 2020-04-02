using System;
using System.Collections.Generic;
using System.Text;

namespace SftLib.Data.Domain.Models
{
   public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        
        
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
