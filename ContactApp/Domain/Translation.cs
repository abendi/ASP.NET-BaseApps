using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Translation
    {
        public int TranslationId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(40960)]
        public string Value { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [ForeignKey(nameof(MultiLangString))]
        public int MultiLangStringId { get; set; }
        public virtual MultiLangString MultiLangString { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(12)]
        public string Culture { get; set; }
    }
}