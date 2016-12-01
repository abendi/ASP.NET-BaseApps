using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(128, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.CategoryName), ResourceType = typeof(Resources.Domain))]
        public string CategoryName { get; set; }

    }
}