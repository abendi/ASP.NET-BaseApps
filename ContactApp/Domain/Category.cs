using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryName))]
        [Display(Name = nameof(Resources.Domain.CategoryNameId), ResourceType = typeof(Resources.Domain))]
        public int CategoryNameId { get; set; }

        [Display(Name = nameof(Resources.Domain.CategoryName), ResourceType = typeof(Resources.Domain))]
        public virtual MultiLangString CategoryName { get; set; }

        [Display(Name = nameof(Resources.Domain.CategoryProducts), ResourceType = typeof(Resources.Domain))]
        public List<Product> Products { get; set; } = new List<Product>(); 
    }
}
