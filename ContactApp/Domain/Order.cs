using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain
{

    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderUser))]
        public int OrderUserId { get; set; }
        public virtual UserInt OrderUser { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        public int OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderPlacedDate { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime OrderForDate { get; set; }
        public DateTime? OrderCompletedDate { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();

        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        public double ProductQuantity { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
