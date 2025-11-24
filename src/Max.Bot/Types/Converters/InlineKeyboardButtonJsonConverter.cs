using System.Text.Json;
using System.Text.Json.Serialization;
using Max.Bot.Types;
using Max.Bot.Types.Enums;

namespace Max.Bot.Types.Converters;

/// <summary>
/// JSON converter for InlineKeyboardButton that handles both new format (type + payload) and legacy format (callback_data).
/// </summary>
public class InlineKeyboardButtonJsonConverter : JsonConverter<InlineKeyboardButton>
{
    /// <inheritdoc />
    public override InlineKeyboardButton Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null!;
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token.");
        }

        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        var button = new InlineKeyboardButton();

        // Read text (required)
        if (root.TryGetProperty("text", out var textElement))
        {
            button.Text = textElement.GetString() ?? string.Empty;
        }

        // Read type (new format)
        if (root.TryGetProperty("type", out var typeElement) && typeElement.ValueKind == JsonValueKind.String)
        {
            var typeString = typeElement.GetString();
            if (Enum.TryParse<ButtonType>(typeString, ignoreCase: true, out var buttonType))
            {
                button.Type = buttonType;
            }
        }

        // Read payload (new format)
        if (root.TryGetProperty("payload", out var payloadElement))
        {
            button.Payload = payloadElement.ValueKind == JsonValueKind.String
                ? payloadElement.GetString()
                : payloadElement.GetRawText();
        }

        // Read url (for link buttons)
        if (root.TryGetProperty("url", out var urlElement))
        {
            button.Url = urlElement.GetString();
            if (button.Type == default(ButtonType) && !string.IsNullOrEmpty(button.Url))
            {
                button.Type = ButtonType.Link;
            }
        }

        // Handle legacy callback_data format (for backward compatibility)
        if (root.TryGetProperty("callback_data", out var callbackDataElement))
        {
            var callbackData = callbackDataElement.GetString();
            if (!string.IsNullOrEmpty(callbackData))
            {
                button.Type = ButtonType.Callback;
                button.Payload = callbackData;
            }
        }

        // If type is still not set, default to Callback if payload is present
        if (button.Type == default(ButtonType) && !string.IsNullOrEmpty(button.Payload))
        {
            button.Type = ButtonType.Callback;
        }

        return button;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, InlineKeyboardButton value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        // Write type (required) - convert to snake_case to match API format
        var typeString = ConvertToSnakeCase(value.Type.ToString());
        writer.WriteString("type", typeString);

        // Write text (required)
        writer.WriteString("text", value.Text);

        // Write payload (for callback and message buttons)
        if (!string.IsNullOrEmpty(value.Payload))
        {
            writer.WriteString("payload", value.Payload);
        }

        // Write url (for link buttons)
        if (!string.IsNullOrEmpty(value.Url))
        {
            writer.WriteString("url", value.Url);
        }

        writer.WriteEndObject();
    }

    private static string ConvertToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var result = new System.Text.StringBuilder();
        result.Append(char.ToLowerInvariant(input[0]));

        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                result.Append('_');
                result.Append(char.ToLowerInvariant(input[i]));
            }
            else
            {
                result.Append(input[i]);
            }
        }

        return result.ToString();
    }
}

