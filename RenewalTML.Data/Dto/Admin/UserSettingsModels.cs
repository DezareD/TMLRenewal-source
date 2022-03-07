using System.ComponentModel.DataAnnotations;

namespace RenewalTML.Data.Dto
{

    public class EditAdminScreenName
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [RegularExpression("^.[^{}:\"]{4,20}$", ErrorMessage = "Неверный ввод.")] // Любые символы от 4 до 20
        public string NewScreenName { get; set; }
    }

    public class EditAdminLogin
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [RegularExpression("^[a-zA-Z0-9.\\s]{4,20}$", ErrorMessage = "Неверный ввод.")]
        public string NewLogin { get; set; }
    }

    public class EditAdminPassword
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        [MaxLength(20, ErrorMessage = "Максимальная длина пароля: 20 символов.")]
        public string NewPassword { get; set; }
    }
}
