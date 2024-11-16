using ChimneyOrderForm.Options;
using ChimneyOrderForm.RaynetOpenApi;
using ChimneyOrderForm.RaynetOpenApi.Options;

using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Pøidání služby session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<LoginCredentials>(builder.Configuration.GetSection("LoginCredentials"));
builder.Services.Configure<RaynetSettings>(builder.Configuration.GetSection("RaynetSettings"));

builder.Services.AddScoped(typeof(RaynetLeadClient));

builder.Services.AddHttpContextAccessor();

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

builder.Services.AddHttpClient<RaynetLeadClient>((serviceProvider, client) =>
{
    IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

    RaynetSettings? raynetSettings = configuration.GetSection(nameof(RaynetSettings))
        .Get<RaynetSettings>();

    IHttpContextAccessor httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();

    string validUserJson = httpContext.HttpContext.Session.GetString("ValidUser");
    UserCredentials validUser = null;
    if (!string.IsNullOrEmpty(validUserJson))
    {
        validUser = JsonSerializer.Deserialize<UserCredentials>(validUserJson)!;
    }

    validUser = validUser ?? new UserCredentials();
    // Nastavení základní URL
    client.BaseAddress = new Uri(raynetSettings.ApiUrl);

    // Základní autentizace
    string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{validUser.Username}:{raynetSettings.ApiKey}"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

    // Hlavièka instance
    client.DefaultRequestHeaders.Add("X-Instance-Name", raynetSettings.InstanceName);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.Run();