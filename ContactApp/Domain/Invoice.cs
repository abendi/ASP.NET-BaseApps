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
        public int InvoiceId { get; set; }

        public bool InvoicePaymentReceived { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? InvoicePaymentReceivedDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "money")]
        public decimal InvoiceTotalSum { get; set; }
    }
}
