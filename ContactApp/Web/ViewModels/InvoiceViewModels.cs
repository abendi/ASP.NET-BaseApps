﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.ViewModels
{
    public class InvoiceDetailViewModel
    {
        public Invoice Invoice { get; set; }

        public Order Order { get; set; }


    }
}