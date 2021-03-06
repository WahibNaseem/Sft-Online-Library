﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftLibrary.API.Resources
{
    public class BookCheckoutResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public IEnumerable<CheckoutHistoryResource> CheckoutHistories { get; set; }

        public StatusResource Status { get; set; }
    }
}
