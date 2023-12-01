using System.ComponentModel.DataAnnotations;

namespace ContactsManage.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do contato.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o email do contato.")]
        [EmailAddress(ErrorMessage = "Email informado inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o celular do contato.")]
        [Phone(ErrorMessage = "Celular informado inválido")]
        public string CellPhone { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}
