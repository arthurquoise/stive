using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace stive.Models
{
    public partial class Address
    {
        [Key]
        public int AddressId { get; set; }

        [DataType(DataType.Text)]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }

        [DataType(DataType.Text)]
        public string Address1 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
