using System;
using System.Collections.Generic;
using System.Text;

namespace SftLib.Data.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string TelePhone { get; set; }
        public string Address { get; set; }
    }
}
