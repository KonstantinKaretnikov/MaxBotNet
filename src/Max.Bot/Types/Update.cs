// рџ“Ѓ [Update] - РњРѕРґРµР»СЊ РѕР±РЅРѕРІР»РµРЅРёСЏ Max Messenger
// рџЋЇ Core function: РџСЂРµРґСЃС‚Р°РІР»СЏРµС‚ РѕР±РЅРѕРІР»РµРЅРёРµ РѕС‚ Max Messenger
// рџ”— Key dependencies: System.Text.Json.Serialization, System.ComponentModel.DataAnnotations, Max.Bot.Types, Max.Bot.Types.Enums
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РґР»СЏ РїРѕР»СѓС‡РµРЅРёСЏ РѕР±РЅРѕРІР»РµРЅРёР№ РѕС‚ Max Messenger API

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Max.Bot.Types.Enums;

namespace Max.Bot.Types;

/// <summary>
/// Represents an update from Max Messenger.
/// </summary>
public class Update
{
    /// <summary>
    /// Gets or sets the unique identifier of the update.
    /// </summary>
    /// <value>The unique identifier of the update.</value>
    [Range(1, long.MaxValue, ErrorMessage = "Update ID must be greater than zero.")]
    [JsonPropertyName("updateId")]
    public long UpdateId { get; set; }

    /// <summary>
    /// Gets or sets the type of the update.
    /// </summary>
    /// <value>The type of the update (message or callback_query).</value>
    [JsonPropertyName("type")]
    public UpdateType Type { get; set; }

    /// <summary>
    /// Gets or sets the message in this update (if type is Message).
    /// </summary>
    /// <value>The message in this update, or null if not available.</value>
    [JsonPropertyName("message")]
    public Message? Message { get; set; }

    /// <summary>
    /// Gets or sets the callback query in this update (if type is CallbackQuery).
    /// </summary>
    /// <value>The callback query in this update, or null if not available.</value>
    [JsonPropertyName("callbackQuery")]
    public CallbackQuery? CallbackQuery { get; set; }
}

