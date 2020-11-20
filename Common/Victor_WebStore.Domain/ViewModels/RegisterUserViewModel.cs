using System.ComponentModel.DataAnnotations;

namespace Victor_WebStore.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256), MinLength(2)]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
