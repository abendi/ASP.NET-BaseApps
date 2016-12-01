using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [StringLength(128, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        public string QuestionAsker { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(128, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resources.Common.EmailValidationError), ErrorMessageResourceType = typeof(Resources.Common))]
        public string QuestionAskerEmail { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(128, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        public string QuestionSubject { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(16384, ErrorMessageResourceName = nameof(Resources.Common.FieldMaxLength), ErrorMessageResourceType = typeof(Resources.Common))]
        public string QuestionBody { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime QuestionSubmittedTime { get; set; }
    }
}
