using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SftLibrary.API.Resources
{
    public class BookResource
    {
        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Year { get; set; }

        public StatusResource Status { get; set; }

    }
}
