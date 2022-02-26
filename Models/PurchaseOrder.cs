using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace stive.Models
{
    public partial class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? OrderDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? SubTotal { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }

        [ForeignKey("VendorId")]
        public int? VendorId { get; set; }

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
        public virtual Product Product { get; set; }
        public virtual Status Status { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
