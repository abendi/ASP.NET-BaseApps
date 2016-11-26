using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Areas.Admin.ViewModels
{
    public class IngredientIndexViewModel
    {
        public List<Ingredient> Ingredients { get; set; } 
    }

    public class IngredientCreateEditViewModel
    {
        public Ingredient Ingredient { get; set; }
        public string IngredientName { get; set; }

        public bool HasGluten { get; set; }

        public bool IsAllergenic { get; set; }
    }
}