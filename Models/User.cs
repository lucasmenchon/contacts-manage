using ContactsManage.Enums;
using ContactsManage.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ContactsManage.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário.")]
        public string Password { get; set; }

        //public Address Address { get; set; }

        [Required(ErrorMessage = "Digite o email do usuário.")]
        [EmailAddress(ErrorMessage = "Email informado inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de perfil.")]
        public eProfile? Profile { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual List<Contact>? Contacts { get; set; }

        public bool ValidPassword(string password)
        {
            return Password == password.MakeHash();
        }

        public void SetHashPassword()
        {
            Password = Password.MakeHash();
        }

        public void SetNewPassword(string newPassword)
        {
            Password = newPassword.MakeHash();
        }

        public string MakeNewPassword()
        {
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            Password = newPassword.MakeHash();
            return newPassword;
        }
    }
}
