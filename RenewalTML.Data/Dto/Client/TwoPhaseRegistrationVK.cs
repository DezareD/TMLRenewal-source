using System.ComponentModel.DataAnnotations;

namespace RenewalTML.Data.Dto
{
    public class TwoPhaseRegistrationVK
    {
        [Required(ErrorMessage = "Обязательно для заполнения.")]
        //[MinLength(3, ErrorMessage = "Имя должно содержать минимум 3 символа.")]
        //[MaxLength(20, ErrorMessage = "Максимальная длина имени: 20 символов.")]
        [RegularExpression("^[a-zA-Z0-9.\\s]{4,20}$", ErrorMessage = "Неверный ввод.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения.")]
        //[MinLength(3, ErrorMessage = "Имя должно содержать минимум 3 символа.")]
        //[MaxLength(20, ErrorMessage = "Максимальная длина имени: 20 символов.")]
        [RegularExpression("^.[^{}:\"]{4,20}$", ErrorMessage = "Неверный ввод.")] // Любые символы от 4 до 20
        public string ScreenName { get; set; }

        [Required(ErrorMessage = "Обязательно для заполнения.")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать минимум 6 символов.")]
        [MaxLength(20, ErrorMessage = "Максимальная длина пароля: 20 символов.")]
        public string Password { get; set; }
    }
}
