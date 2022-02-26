using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace stive.Models
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        [Key]
        public int SalesOrderId { get; set; }
        public decimal SubTotal { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
        public int ProductQuantity { get; set; }


        [ForeignKey("StatusId")]
        public int StatusId { get; set; }

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
