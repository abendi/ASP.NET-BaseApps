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
        [Key]
        public int TranslationId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(40960, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Common.Value), ResourceType = typeof(Resources.Common))]
        public string Value { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [ForeignKey(nameof(MultiLangString))]
        [Display(Name = nameof(Resources.Domain.MultiLangStringId), ResourceType = typeof(Resources.Domain))]
        public int MultiLangStringId { get; set; }
        public virtual MultiLangString MultiLangString { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(12, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.TranslationCulture), ResourceType = typeof(Resources.Domain))]
        public string Culture { get; set; }
    }
}