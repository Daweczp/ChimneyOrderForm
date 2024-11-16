using ChimneyOrderForm.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

namespace ChimneyOrderForm.Pages;

public class IndexModel : PageModel
{
    private const string ValidPassword = "password123";
    private const string ValidUsername = "admin";
    private readonly List<UserCredentials> _loginCredentials;

    public IndexModel(IConfiguration configuration)
    {
        _loginCredentials = configuration.GetSection("LoginCredentials").Get<List<UserCredentials>>();
    }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string Username { get; set; }

    public IActionResult OnPost()
    {
        UserCredentials? validUser = _loginCredentials
            .FirstOrDefault(user => user.Username == Username && user.Password == Password);

        if (validUser != null)
        {
            string validUserJson = JsonSerializer.Serialize(validUser);

            HttpContext.Session.SetString("IsLoggedIn", "true");
            HttpContext.Session.SetString("ValidUser", validUserJson);

            return RedirectToPage("/OrderForm");
        }

        ModelState.AddModelError(string.Empty, "Neplatné uživatelské jméno nebo heslo.");
        return Page();
    }
}