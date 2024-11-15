using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;

public class OrderFormModel : PageModel
{
    [BindProperty]
    [Required]
    public Dictionary<string, AddressModel> Address { get; set; } = new();

    [BindProperty]
    public DateTime? BirthDate { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    [Required]
    public string FullName { get; set; }

    [BindProperty]
    [Required]
    public int GasFuelCount { get; set; }

    [BindProperty]
    public string? Ico { get; set; }

    [BindProperty]
    public string? Note { get; set; }

    [BindProperty]
    [Required]
    public string PhoneNumber { get; set; }

    [BindProperty]
    [Required]
    public string Referrer { get; set; }

    [BindProperty]
    [Required]
    public int SolidFuelCount { get; set; }

    public string SuccessMessage { get; set; }

    [BindProperty]
    [Required]
    public string ZkType { get; set; }

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("IsLoggedIn") != "true")
        {
            return RedirectToPage("/Index");
        }

        InicializaceAdres();
        return Page();
    }

    public IActionResult OnPost()
    {
        // Dynamick� validace pro I�O a Datum narozen� na z�klad� hodnoty ZkType
        if (ZkType == "Firma" && string.IsNullOrWhiteSpace(Ico))
        {
            ModelState.AddModelError(nameof(Ico), "I�O je povinn�, pokud je vybr�n typ ZK Firma.");
        }
        else if (ZkType == "Fyzick� osoba" && !BirthDate.HasValue)
        {
            ModelState.AddModelError(nameof(BirthDate), "Datum narozen� je povinn�, pokud je vybr�n typ ZK Fyzick� osoba.");
        }

        if (!ModelState.IsValid)
        {
            // Pokud jsou chyby ve validaci, vr�t�me formul�� zp�t s chybov�mi zpr�vami
            InicializaceAdres();
            return Page();
        }

        try
        {
            // Zde m��ete p�idat k�d pro ukl�d�n� dat do datab�ze nebo jin� proces ulo�en� SaveOrderToDatabase();

            SuccessMessage = "Formul�� byl �sp�n� odesl�n.";
            ResetFormModel();
            InicializaceAdres();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Do�lo k chyb� p�i zpracov�n� formul��e. Zkuste to znovu.");
        }

        return Page();
    }

    private void InicializaceAdres()
    {
        Address["Adresa �i�t�n�"] = new AddressModel();
        Address["Adresa trval�ho bydli�t�"] = new AddressModel();
        Address["Adresa koresponden�n�"] = new AddressModel();
    }

    private void ResetFormModel()
    {
        PhoneNumber = string.Empty;
        FullName = string.Empty;
        ZkType = string.Empty;
        BirthDate = null;
        Ico = string.Empty;
        Email = string.Empty;
        SolidFuelCount = 0;
        GasFuelCount = 0;
        Note = string.Empty;
        Referrer = string.Empty;
        Address = new Dictionary<string, AddressModel>();
    }

    public class AddressModel
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}