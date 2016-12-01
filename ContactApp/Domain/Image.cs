using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image : BaseEntity
    {
        [Key]
        public int ImageId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(128, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.ImageUrl), ResourceType = typeof(Resources.Domain))]
        [RegularExpression(@"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$", ErrorMessageResourceName = nameof(Resources.Domain.ImageUrlInvalid), ErrorMessageResourceType = typeof(Resources.Domain))]
        public string ImageUrl { get; set; }


    }
}
