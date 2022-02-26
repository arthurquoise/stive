using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace stive.Models
{
    public partial class Discount
    {
        [Key]
        public int DiscountId { get; set; }
        public string Name { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool Active { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
