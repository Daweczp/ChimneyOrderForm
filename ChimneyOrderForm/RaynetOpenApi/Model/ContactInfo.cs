using System.Text.Json.Serialization;

namespace ChimneyOrderForm.RaynetOpenApi.Model;

public class ContactInfo
{
    [JsonPropertyName("doNotSendMM")]
    public bool DoNotSendMM { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("email2")]
    public string Email2 { get; set; }

    [JsonPropertyName("fax")]
    public string Fax { get; set; }

    [JsonPropertyName("otherContact")]
    public string OtherContact { get; set; }

    [JsonPropertyName("tel1")]
    public string Tel1 { get; set; }

    [JsonPropertyName("tel1Type")]
    public string Tel1Type { get; set; }

    [JsonPropertyName("tel2")]
    public string Tel2 { get; set; }

    [JsonPropertyName("tel2Type")]
    public string Tel2Type { get; set; }

    [JsonPropertyName("www")]
    public string Www { get; set; }
}