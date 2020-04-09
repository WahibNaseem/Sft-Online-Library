using SftLib.Data.Domain.Models;
using System;

namespace SftLibrary.Data.Domain.Models
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
    }
}
