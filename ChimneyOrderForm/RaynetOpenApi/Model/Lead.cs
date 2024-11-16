using System.Text.Json.Serialization;

namespace ChimneyOrderForm.RaynetOpenApi.Model;

public class Lead
{
    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    [JsonPropertyName("category")]
    public int? Category { get; set; }

    [JsonPropertyName("companyName")]
    public string? CompanyName { get; set; }

    [JsonPropertyName("contactInfo")]
    public ContactInfo? ContactInfo { get; set; }

    [JsonPropertyName("contactSource")]
    public int? ContactSource { get; set; }

    [JsonPropertyName("customFields")]
    public CustomFields? CustomFields { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("leadPhase")]
    public int? LeadPhase { get; set; }

    [JsonPropertyName("notice")]
    public string? Notice { get; set; }

    [JsonPropertyName("notificationEmailAddresses")]
    public List<string>? NotificationEmailAddresses { get; set; }

    [JsonPropertyName("notificationMessage")]
    public string? NotificationMessage { get; set; }

    [JsonPropertyName("owner")]
    public int? Owner { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("regNumber")]
    public string? RegNumber { get; set; }

    [JsonPropertyName("socialNetworkContact")]
    public SocialNetworkContact? SocialNetworkContact { get; set; }

    [JsonPropertyName("tags")]
    public string? Tags { get; set; }

    [JsonPropertyName("topic")]
    public string? Topic { get; set; }
}