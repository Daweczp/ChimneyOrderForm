using ChimneyOrderForm.RaynetOpenApi;
using ChimneyOrderForm.RaynetOpenApi.Model;
using ChimneyOrderForm.RaynetOpenApi.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

public class OrderFormModel : PageModel
{
    private readonly RaynetLeadClient _leadClient;
    private readonly RaynetSettings _raynetSettings;

    public OrderFormModel(RaynetLeadClient leadClient, IOptions<RaynetSettings> raynetSettings)
    {
        _leadClient = leadClient;
        _raynetSettings = raynetSettings.Value!;
    }

    [BindProperty]
    [Required]
    public Dictionary<string, AddressModel> Address { get; set; } = new();

    [BindProperty]
    public DateTime? BirthDate { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    [Required]
    public string FirstName { get; set; }

    [BindProperty]
    [Required]
    public int GasFuelCount { get; set; }

    [BindProperty]
    public string? Ico { get; set; }

    [BindProperty]
    [Required]
    public string LastName { get; set; }

    [BindProperty]
    public string? Note { get; set; }

    [BindProperty]
    [Required]
    public string PhoneNumber { get; set; }

    [BindProperty]
    public string Priority { get; set; }

    //[BindProperty]
    //[Required]
    //public string Referrer { get; set; }

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

    public async Task<IActionResult> OnPost()
    {
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
            InicializaceAdres();
            return Page();
        }

        try
        {
            AddressModel address = Address["Adresa �i�t�n�"];
            Lead l = new()
            {
                Priority = Priority,
                Topic = _raynetSettings.LeadTitle,
                Address = new Address
                {
                    City = address.City,
                    Street = $"{address.Street} {address.HouseNumber}",
                    ZipCode = address.ZipCode
                },
                FirstName = FirstName,
                LastName = LastName,
                RegNumber = BirthDate?.ToShortDateString() ?? Ico,
                Notice =
                    $"Telefon: {PhoneNumber}, Po�et tuh�ch paliv: {SolidFuelCount}, Po�et plynn�ch paliv: {GasFuelCount}, Pozn�mka: {Note}"
            };

            await _leadClient.CreateLead(l);

            ResetFormModel();
            InicializaceAdres();
            SuccessMessage = "Formul�� byl �sp�n� odesl�n.";
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
        //Address["Adresa trval�ho bydli�t�"] = new AddressModel();
        //Address["Adresa koresponden�n�"] = new AddressModel();
    }

    private void ResetFormModel()
    {
        PhoneNumber = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        ZkType = string.Empty;
        BirthDate = null;
        Ico = string.Empty;
        Email = string.Empty;
        SolidFuelCount = 0;
        GasFuelCount = 0;
        Note = string.Empty;
        //Referrer = string.Empty;
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