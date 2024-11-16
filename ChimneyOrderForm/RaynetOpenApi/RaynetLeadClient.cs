using ChimneyOrderForm.RaynetOpenApi.Model;

namespace ChimneyOrderForm.RaynetOpenApi;

public class RaynetLeadClient
{
    private const string Prefix = "lead";
    private readonly HttpClient _httpClient;

    public RaynetLeadClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateLead(Lead lead)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync(Prefix, lead);
        response.EnsureSuccessStatusCode();

        string result = await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetLeads()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(Prefix);
        response.EnsureSuccessStatusCode();

        string result = await response.Content.ReadAsStringAsync();
        return result;
    }
}