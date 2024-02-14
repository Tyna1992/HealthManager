using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HealthManagerServer.Model;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female,
    Other
}