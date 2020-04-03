using System;
using System.Collections.Generic;
using System.Text;

namespace SftLib.Data.Domain.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Book  Book{ get; set; }
    }
}
