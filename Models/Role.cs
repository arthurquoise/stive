using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace stive.Models
{
    public partial class Role
    {
        public Role()
        {
            People = new HashSet<Person>();
        }

        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
