using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace logindlm.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Az email mező kitöltése kötelező!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A jelszó mező kitöltése kötelező!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

        
