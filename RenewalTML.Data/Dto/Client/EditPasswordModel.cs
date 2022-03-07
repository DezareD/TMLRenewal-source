using System.ComponentModel.DataAnnotations;

namespace RenewalTML.Data.Dto
{
    public class EditPasswordModel
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        [MaxLength(20, ErrorMessage = "Максимальная длина пароля: 20 символов.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        [MaxLength(20, ErrorMessage = "Максимальная длина пароля: 20 символов.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [Compare("NewPassword", ErrorMessage = "Не совпадает с новым паролём.")]
        public string NewPasswordRewrite { get; set; }
    }
}
