using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stive.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Mot de passe")]
        public string Password { get; set; }
        [DisplayName("Confirmation du mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passes ne correspondent pas !")]
        public string ConfirmPasssword { get; set; }
    }
}
