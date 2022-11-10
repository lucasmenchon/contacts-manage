using System.ComponentModel.DataAnnotations;

namespace ContactsManage.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o email do contato.")]
        [EmailAddress(ErrorMessage = "Email informado inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato.")]
        [Phone(ErrorMessage = "Celular informado inválido")]
        public string Celular { get; set; }

        public int? UserId { get; set; }

        public UserModel? User { get; set; }

    }
}
