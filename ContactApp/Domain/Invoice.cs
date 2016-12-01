using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Invoice
    {
        [Key]
        [Display(Name = nameof(Resources.Domain.InvoiceId), ResourceType = typeof(Resources.Domain))]
        public int InvoiceId { get; set; }

        /*
            [Display(Name = nameof(Resources.Domain.InvoicePaymentReceived), ResourceType = typeof(Resources.Domain))]
            public bool InvoicePaymentReceived { get; set; }
        */
        [DataType(DataType.DateTime, ErrorMessageResourceName = nameof(Resources.Common.FieldMustBeDataTypeDateTime), ErrorMessageResourceType = typeof(Resources.Common))]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(Resources.Domain.InvoicePaymentReceived), ResourceType = typeof(Resources.Domain))]
        public DateTime? InvoicePaymentReceivedDate { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(Resources.Domain.InvoiceTotalSum), ResourceType = typeof(Resources.Domain))]
        public decimal InvoiceTotalSum { get; set; }
    }
}
