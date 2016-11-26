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
        public int IngredientId { get; set; }

        [ForeignKey((nameof(IngredientName)))]
        public int IngredientNameId { get; set; }

        public virtual MultiLangString IngredientName { get; set; }

        public bool HasGluten { get; set; }

        public bool IsAllergenic { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
