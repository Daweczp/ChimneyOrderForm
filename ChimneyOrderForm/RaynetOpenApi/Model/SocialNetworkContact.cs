using System.Text.Json.Serialization;

namespace ChimneyOrderForm.RaynetOpenApi.Model;

public class SocialNetworkContact
{
    [JsonPropertyName("facebook")]
    public string Facebook { get; set; }

    [JsonPropertyName("googleplus")]
    public string GooglePlus { get; set; }

    [JsonPropertyName("instagram")]
    public string Instagram { get; set; }

    [JsonPropertyName("linkedin")]
    public string LinkedIn { get; set; }

    [JsonPropertyName("pinterest")]
    public string Pinterest { get; set; }

    [JsonPropertyName("skype")]
    public string Skype { get; set; }

    [JsonPropertyName("twitter")]
    public string Twitter { get; set; }

    [JsonPropertyName("youtube")]
    public string YouTube { get; set; }
}