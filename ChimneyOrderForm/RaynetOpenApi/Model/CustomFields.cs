using System.Text.Json.Serialization;

namespace ChimneyOrderForm.RaynetOpenApi.Model;

public class CustomFields
{
    [JsonPropertyName("VIP_b91d1")]
    public bool VIP { get; set; }
}