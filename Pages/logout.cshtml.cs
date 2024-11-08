using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace logindlm.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet() 
        {
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            // Kijelentkezés
            await signInManager.SignOutAsync();
            return RedirectToPage("Login");
        }

        public IActionResult OnPostDontLogoutAsync()
        {
            // Nem jelentkezik ki, csak visszatér a termek oldalra
            return RedirectToPage("termek");
        }

    }
}

