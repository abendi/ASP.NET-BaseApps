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
        [Key]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductName))]
        public int ProductNameId { get; set; }

        [Display(Name = nameof(Resources.Domain.ProductName), ResourceType = typeof(Resources.Domain))]
        public virtual MultiLangString ProductName { get; set; }

        [ForeignKey(nameof(ProductDescription))]
        public int ProductDescriptionId { get; set; }

        [Display(Name = nameof(Resources.Domain.ProductDescription), ResourceType = typeof(Resources.Domain))]
        public virtual MultiLangString ProductDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [Column(TypeName = "money")]
        [Range(0.0, 10000.0, ErrorMessageResourceName = nameof(Resources.Domain.ProductBasePriceRange), ErrorMessageResourceType = typeof(Resources.Domain))]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(Resources.Domain.ProductBasePrice), ResourceType = typeof(Resources.Domain))]
        public decimal BasePrice { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Display(Name = nameof(Resources.Domain.ProductCategory), ResourceType = typeof(Resources.Domain))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Image))]
        public int? ImageId { get; set; }

        [Display(Name = nameof(Resources.Domain.ProductImage), ResourceType = typeof(Resources.Domain))]
        public virtual Image Image { get; set; }

        [Display(Name = nameof(Resources.Domain.ProductIngredients), ResourceType = typeof(Resources.Domain))]
        public virtual List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        
        public virtual List<Order> Orders { get; set; } = new List<Order>();  
    }
}
