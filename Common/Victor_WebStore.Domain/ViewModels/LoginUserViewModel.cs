using System.ComponentModel.DataAnnotations;

namespace Victor_WebStore.Domain.ViewModels
{
    public class LoginUserViewModel
    {
        [Required, MaxLength(256), MinLength(2)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
