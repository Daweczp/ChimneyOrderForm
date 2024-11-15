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
        // Dynamická validace pro IÈO a Datum narození na základì hodnoty ZkType
        if (ZkType == "Firma" && string.IsNullOrWhiteSpace(Ico))
        {
            ModelState.AddModelError(nameof(Ico), "IÈO je povinné, pokud je vybrán typ ZK Firma.");
        }
        else if (ZkType == "Fyzická osoba" && !BirthDate.HasValue)
        {
            ModelState.AddModelError(nameof(BirthDate), "Datum narození je povinné, pokud je vybrán typ ZK Fyzická osoba.");
        }

        if (!ModelState.IsValid)
        {
            // Pokud jsou chyby ve validaci, vrátíme formuláø zpìt s chybovými zprávami
            InicializaceAdres();
            return Page();
        }

        try
        {
            // Zde mùžete pøidat kód pro ukládání dat do databáze nebo jiný proces uložení SaveOrderToDatabase();

            SuccessMessage = "Formuláø byl úspìšnì odeslán.";
            ResetFormModel();
            InicializaceAdres();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Došlo k chybì pøi zpracování formuláøe. Zkuste to znovu.");
        }

        return Page();
    }

    private void InicializaceAdres()
    {
        Address["Adresa èištìní"] = new AddressModel();
        Address["Adresa trvalého bydlištì"] = new AddressModel();
        Address["Adresa korespondenèní"] = new AddressModel();
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