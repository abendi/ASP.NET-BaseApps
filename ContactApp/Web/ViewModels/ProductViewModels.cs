using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;

namespace Web.ViewModels
{
    public class ProductOrderViewModel
    {
        public Product Product { get; set; }
        public Order Order { get; set; }
        
    }
}