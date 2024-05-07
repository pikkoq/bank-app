using System.Text.Json.Serialization;

namespace Bank_app_api.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]

    public enum UserType
    {
        User = 1,
        Admin = 2,
    }
}
