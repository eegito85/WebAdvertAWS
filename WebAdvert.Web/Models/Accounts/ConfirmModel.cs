using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert.Web.Models.Accounts
{
    public class ConfirmModel
    {
        [Required(ErrorMessage ="Email é um campo obrigatório")]
        [Display(Name="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Código é um campo obrigatório")]
        [Display(Name ="Código")]
        public string Code { get; set; }
    }
}
