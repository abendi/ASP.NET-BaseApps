using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotations;

namespace Domain
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductName))]
        public int ProductNameId { get; set; }

        public virtual MultiLangString ProductName { get; set; }

        [ForeignKey(nameof(ProductDescription))]
        public int ProductDescriptionId { get; set; }

        public virtual MultiLangString ProductDescription { get; set; }

        [Column(TypeName = "money")]
        public decimal BasePrice { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Image))]
        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; } = new List<Ingredient>(); 
    }
}
