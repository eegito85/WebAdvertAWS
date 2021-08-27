using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.Accounts
{
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email do usuário")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(8,ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
        [Display(Name ="Senha")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Senha e confirmação de senha não conferem")]
        [Display(Name ="Confirme sua senha")]
        public string ConfirmPassword { get; set; }
    }
}
