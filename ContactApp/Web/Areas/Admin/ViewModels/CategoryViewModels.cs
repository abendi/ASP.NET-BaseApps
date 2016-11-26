using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class CategoryIndexViewModel
    {
        public List<Category> Categories { get; set; }
    }

    public class CategoryCreateEditViewModel
    {
        public Category Category { get; set; }

        public string CategoryName { get; set; }

    }
}