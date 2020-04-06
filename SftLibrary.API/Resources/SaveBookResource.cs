using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftLibrary.API.Resources
{
    public class SaveBookResource
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public int StatusId { get; set; } = -1;
    }
}
