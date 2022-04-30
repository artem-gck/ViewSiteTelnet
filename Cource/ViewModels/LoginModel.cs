using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cource.ViewModels
{
    public class LoginModel
    {
        public int Id { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Роль")]
        public string Role { get; set; }

        [DisplayName("Изменение новостей")]
        public bool NewsMaker { get; set; }
    }
}
