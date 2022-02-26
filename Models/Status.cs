using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace stive.Models
{
    public partial class Status
    {
        public Status()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
