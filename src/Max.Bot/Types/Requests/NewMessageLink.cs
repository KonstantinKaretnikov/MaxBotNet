// рџ“Ѓ [NewMessageLink] - РЎСЃС‹Р»РєР° РЅР° СЃРѕРѕР±С‰РµРЅРёРµ РґР»СЏ РїРµСЂРµСЃС‹Р»РєРё/РѕС‚РІРµС‚Р°
// рџЋЇ Core function: РџСЂРµРґСЃС‚Р°РІР»СЏРµС‚ СЃСЃС‹Р»РєСѓ РЅР° СЃРѕРѕР±С‰РµРЅРёРµ РїСЂРё РѕС‚РїСЂР°РІРєРµ РЅРѕРІРѕРіРѕ СЃРѕРѕР±С‰РµРЅРёСЏ
// рџ”— Key dependencies: System.Text.Json.Serialization, System.ComponentModel.DataAnnotations
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ SendMessageRequest РґР»СЏ РїРµСЂРµСЃС‹Р»РєРё РёР»Рё РѕС‚РІРµС‚Р° РЅР° СЃРѕРѕР±С‰РµРЅРёРµ

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Max.Bot.Types.Requests;

/// <summary>
/// Represents a link to a message for forwarding or replying.
/// </summary>
public class NewMessageLink
{
    /// <summary>
    /// Gets or sets the unique identifier of the message to link to.
    /// </summary>
    /// <value>The unique identifier of the message.</value>
    [Range(1, long.MaxValue, ErrorMessage = "Message ID must be greater than zero.")]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the chat containing the message.
    /// </summary>
    /// <value>The unique identifier of the chat, or null if not specified.</value>
    [Range(1, long.MaxValue, ErrorMessage = "Chat ID must be greater than zero.")]
    [JsonPropertyName("chatId")]
    public long? ChatId { get; set; }
}

