using System.ComponentModel.DataAnnotations;

namespace logindlm.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "Az email mező kitöltése kötelező!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A jelszó mező kitöltése kötelező!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "A jelszó megerősítése kötelező!")]
        [Compare(nameof(Password), ErrorMessage = "A jelszó nem egyezik!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
