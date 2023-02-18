using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Contact.API.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ReportStatus
    {
        Waiting = 1,
        InProgress = 2,
        Completed = 3
    }
}
