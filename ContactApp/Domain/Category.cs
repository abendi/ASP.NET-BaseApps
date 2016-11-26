using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryName))]
        public int CategoryNameId { get; set; }

        public virtual MultiLangString CategoryName { get; set; }

        public List<Product> Products { get; set; } = new List<Product>(); 
    }
}
