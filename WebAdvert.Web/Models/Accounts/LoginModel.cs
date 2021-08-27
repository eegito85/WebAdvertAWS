using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.Accounts
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email é obrigatório")]
        [EmailAddress]
        [Display(Name ="E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Senha é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name ="Senha")]
        public string Password { get; set; }

        [Display(Name="Lembrar meus dados")]
        public bool RememberMe { get; set; }
    }
}
