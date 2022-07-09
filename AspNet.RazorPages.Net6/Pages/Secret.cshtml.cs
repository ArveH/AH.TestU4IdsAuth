using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.RazorPages.Net6.Pages;

[Authorize] // Can also add an AuthorizeFilter to a page/folder in Program.cs
public class SecretModel : PageModel
{
    private readonly ILogger<SecretModel> _logger;

    public SecretModel(ILogger<SecretModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("You got to the secret page");
    }
}