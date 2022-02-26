using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace stive.Models
{
    public partial class Person
    {
        public Person()
        {
            Addresses = new HashSet<Address>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        [Key]
        public int PersonId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
