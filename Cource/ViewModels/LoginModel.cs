using System.ComponentModel.DataAnnotations;

namespace Cource.ViewModels
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        public bool NewsMaker { get; set; }
    }
}
