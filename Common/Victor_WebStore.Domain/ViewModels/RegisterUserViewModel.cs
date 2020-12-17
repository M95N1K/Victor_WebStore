using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Victor_WebStore.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required] 
        [MaxLength(256), MinLength(2, ErrorMessage = "Минимальная длина 2 символа")]
        [Display(Name = "Имя пользователя")]
        [Remote("IsNameFree","Account")]
        public string UserName { get; set; }

        [Required] 
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
