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
        [Key]
        [Display(Name = nameof(Resources.Domain.OrderId), ResourceType = typeof(Resources.Domain))]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderUser))]
        public int OrderUserId { get; set; }

        [Display(Name = nameof(Resources.Domain.OrderUser), ResourceType = typeof(Resources.Domain))]
        public virtual UserInt OrderUser { get; set; }

        [ForeignKey(nameof(OrderStatus))]
        public int OrderStatusId { get; set; }

        [Display(Name = nameof(Resources.Domain.OrderStatus), ResourceType = typeof(Resources.Domain))]
        public virtual OrderStatus OrderStatus { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = nameof(Resources.Domain.OrderPlacedDate), ResourceType = typeof(Resources.Domain))]
        public DateTime OrderPlacedDate { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resources.Domain.OrderForDateRequired), ErrorMessageResourceType = typeof(Resources.Domain))]
        [DataType(DataType.DateTime, ErrorMessageResourceName = nameof(Resources.Common.FieldMustBeDataTypeDateTime), ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.OrderForDate), ResourceType = typeof(Resources.Domain))]
        public DateTime OrderForDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = nameof(Resources.Domain.OrderCompletedDate), ResourceType = typeof(Resources.Domain))]
        public DateTime? OrderCompletedDate { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();

        [Required(ErrorMessageResourceName = nameof(Resources.Common.FieldIsRequired), ErrorMessageResourceType = typeof(Resources.Common))]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(Resources.Domain.OrderProductQuantity), ResourceType = typeof(Resources.Domain))]
        [Range(1, 20, ErrorMessageResourceName = nameof(Resources.Domain.OrderProductQunatityRange), ErrorMessageResourceType = typeof(Resources.Domain))]
        public double ProductQuantity { get; set; }

        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }

        [Display(Name = nameof(Resources.Domain.OrderInvoice), ResourceType = typeof(Resources.Domain))]
        public virtual Invoice Invoice { get; set; }
    }
}
