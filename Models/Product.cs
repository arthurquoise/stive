using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace stive.Models
{
    public partial class Product
    {
        public Product()
        {
            Discounts = new HashSet<Discount>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Image { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ProductDate { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("VendorId")]
        public int VendorId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public int ProductCategoryId { get; set; }

        [ForeignKey("BrandId")]
        public int BrandId { get; set; }

        public virtual ProductBrand Brand { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
