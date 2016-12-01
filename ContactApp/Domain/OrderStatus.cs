using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    //Alternative to enum storing
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }

        [ForeignKey(nameof(OrderStatusName))]
        public int OrderStatusNameId { get; set; }
        public virtual MultiLangString OrderStatusName { get; set; }
        public int LogicalOrder { get; set; }
        public virtual List<Order> Orders { get; set; } = new List<Order>();
        public enum StatusOrder
        {
            AwaitingPayment = 1,
            InProgress = 2,
            WaitingHandOver = 3,
            Delivered = 4
        }
    }
}
