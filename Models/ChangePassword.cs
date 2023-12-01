using System.ComponentModel.DataAnnotations;

namespace ContactsManage.Models
{
    public class ChangePassword
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Digite a S senha")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha")]
        [Compare("NewPassword", ErrorMessage = "Senha nova não confirmada!")]
        public string ConfirmNewPassword { get; set; }
    }
}
