using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace stive.Models
{
    public partial class ProductBrand
    {
        public ProductBrand()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
