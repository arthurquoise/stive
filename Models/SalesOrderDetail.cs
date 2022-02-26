using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace stive.Models
{
    public partial class SalesOrderDetail
    {
        public int ProductId { get; set; }
        public int SalesOrderId { get; set; }
        public decimal LineTotal { get; set; }
        public int OrderQuantity { get; set; }
        public decimal? UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
    }
}
