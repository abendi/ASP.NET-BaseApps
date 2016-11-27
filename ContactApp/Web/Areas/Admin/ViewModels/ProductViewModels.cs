using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAnnotations;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; } 
    }

    public class ProductCreateEditViewModel
    {
        public Product Product { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductBasePrice { get; set; }

        public ImageCreateViewModel ImageVM { get; set; }

        public int ProductCategoryId { get; set; }

        public SelectList Categories { get; set; }

        [MinimumListItems(1, ErrorMessage = "At least 1 item must be selected")]
        public List<int> IngredientsIds { get; set; }
        public MultiSelectList Ingredients { get; set; }
    }

    
}