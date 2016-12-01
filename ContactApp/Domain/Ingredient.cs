using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Ingredient : BaseEntity
    {
        [Key]
        public int IngredientId { get; set; }

        [ForeignKey((nameof(IngredientName)))]
        public int IngredientNameId { get; set; }

        [Display(Name = nameof(Resources.Domain.IngredientName), ResourceType = typeof(Resources.Domain))]
        public virtual MultiLangString IngredientName { get; set; }

        [Display(Name = nameof(Resources.Domain.IngredientHasGluten), ResourceType = typeof(Resources.Domain))]
        public bool HasGluten { get; set; }

        [Display(Name = nameof(Resources.Domain.IngredientIsAllergenic), ResourceType = typeof(Resources.Domain))]
        public bool IsAllergenic { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
