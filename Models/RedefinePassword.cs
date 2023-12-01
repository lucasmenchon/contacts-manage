using System.ComponentModel.DataAnnotations;

namespace ContactsManage.Models
{
    public class RedefinePassword
    {
        [Required(ErrorMessage = "Digite o Login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o Email.")]
        public string Email { get; set; }
    }
}
