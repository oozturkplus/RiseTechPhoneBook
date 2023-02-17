using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Contact.API.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactInfoType
    {
        Phone = 1,
        EMail = 2,
        Address = 3
    }
}
