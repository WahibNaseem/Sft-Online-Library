using SftLib.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SftLibrary.Data.Domain.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public int CheckoutBookId { get; set; }
        public Book Book { get; set; }
        public int CheckoutUserId { get; set; }
        public User User { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}
