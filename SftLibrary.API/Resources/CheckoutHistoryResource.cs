using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftLibrary.API.Resources
{
    public class CheckoutHistoryResource
    {
        public UserForCheckoutResource User { get; set; }
        public DateTime CheckedOut { get; set; }
        public DateTime? CheckedIn { get; set; }
    }
}
