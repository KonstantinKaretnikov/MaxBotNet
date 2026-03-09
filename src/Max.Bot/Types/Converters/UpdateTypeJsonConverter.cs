using System.Text.Json;
using System.Text.Json.Serialization;
using Max.Bot.Types.Enums;
using Max.Bot.Types.Helpers;

namespace Max.Bot.Types.Converters;

/// <summary>
/// JSON converter for <see cref="UpdateType"/> enum to/from snake_case string.
/// </summary>
public class UpdateTypeJsonConverter : JsonConverter<UpdateType>
{
    /// <summary>
    /// Reads a snake_case string and converts it to <see cref="UpdateType"/>.
    /// </summary>
    /// <param name="reader">The JSON reader.</param>
    /// <param name="typeToConvert">The type to convert to.</param>
    /// <param name="options">The JSON serializer options.</param>
    /// <returns>The corresponding <see cref="UpdateType"/> enum value.</returns>
    public override UpdateType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            return UpdateTypeHelper.FromString(value);
        }

        // Fallback for numeric tokens (should not happen per API spec, but handle gracefully)
        if (reader.TokenType == JsonTokenType.Number)
        {
            var numericValue = reader.GetInt32();
            if (numericValue >= 0 && numericValue <= 16)
            {
                return (UpdateType)numericValue;
            }
        }

        return UpdateType.Unknown;
    }

    /// <summary>
    /// Writes <see cref="UpdateType"/> as a snake_case string.
    /// </summary>
    /// <param name="writer">The JSON writer.</param>
    /// <param name="value">The <see cref="UpdateType"/> value to convert.</param>
    /// <param name="options">The JSON serializer options.</param>
    public override void Write(Utf8JsonWriter writer, UpdateType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(UpdateTypeHelper.ToStringValue(value));
    }
}
