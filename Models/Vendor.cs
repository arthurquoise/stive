using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace stive.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            Products = new HashSet<Product>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
        }

        [Key]
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }

        [DataType(DataType.PostalCode)]
        public int Zipcode { get; set; }
        public string City { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
