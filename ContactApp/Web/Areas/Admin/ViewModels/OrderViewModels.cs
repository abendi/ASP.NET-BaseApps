﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class OrderIndexViewModel
    {
        public List<Order> Orders { get; set; } 
    }
}