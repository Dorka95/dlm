using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using logindlm.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace logindlm.Pages
{
    public class loginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public loginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public Login Model { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);

                if (identityResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl) || returnUrl == "/")
                    {
                        return RedirectToPage("/termek");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }

                ModelState.AddModelError("", "Felhasználónév vagy jelszó hibás.");
            }

            return Page();
        }
    }
}
