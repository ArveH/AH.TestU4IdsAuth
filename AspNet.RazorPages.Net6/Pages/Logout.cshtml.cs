using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.RazorPages.Net6.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
            // just to remove compiler warning
            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = "https://localhost:7077"
                },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
