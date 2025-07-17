using System.Text.Json;
using System.Text.Json.Serialization;

namespace TechChallengeFastFood.CleanArch.API.Converters;

/// <summary>
/// Custom JSON converter for DateTimeOffset to handle specific date format.
/// This converter reads and writes DateTimeOffset values in the format "dd-MM-yyyy hh:mm:ss".
/// It is used to ensure consistent serialization and deserialization of DateTimeOffset values
/// </summary>
public class CustomDateTimeConverter : JsonConverter<DateTimeOffset>
{
    private const string DEFAULT_DATE_FORMAT_MASk = "dd-MM-yyyy HH:mm:ss zzz";

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTimeOffset.ParseExact(reader.GetString(), DEFAULT_DATE_FORMAT_MASk, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DEFAULT_DATE_FORMAT_MASk));
    }
}