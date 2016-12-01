using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(128, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.IngredientName), ResourceType = typeof(Resources.Domain))]
        public string IngredientName { get; set; }

        [Display(Name = nameof(Resources.Domain.IngredientHasGluten), ResourceType = typeof(Resources.Domain))]
        public bool HasGluten { get; set; }

        [Display(Name = nameof(Resources.Domain.IngredientIsAllergenic), ResourceType = typeof(Resources.Domain))]
        public bool IsAllergenic { get; set; }
    }
}