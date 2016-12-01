using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Resources;

namespace Domain
{
    public class Article : BaseEntity
    {
        [Key]
        public int ArticleId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [StringLength(255, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.ArticleName), ResourceType = typeof(Resources.Domain))]
        public string ArticleName { get; set; }


        [ForeignKey("ArticleHeadline")]
        [Display(Name = "ArticleHeadline", ResourceType = typeof (Resources.Domain))]
        public int ArticleHeadlineId { get; set; }

        public virtual MultiLangString ArticleHeadline { get; set; }


        [ForeignKey("ArticleBody")]
        [Display(Name = "ArticleBody", ResourceType = typeof (Resources.Domain))]
        public int ArticleBodyId { get; set; }

        public virtual MultiLangString ArticleBody { get; set; }
    }
}