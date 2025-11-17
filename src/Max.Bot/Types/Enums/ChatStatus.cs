// рџ“Ѓ [ChatStatus] - РЎС‚Р°С‚СѓСЃ С‡Р°С‚Р° РІ Max Messenger
// рџЋЇ Core function: РџРµСЂРµС‡РёСЃР»РµРЅРёРµ СЃС‚Р°С‚СѓСЃРѕРІ С‡Р°С‚Р° (active, removed, left, closed)
// рџ”— Key dependencies: System.Text.Json.Serialization
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ РјРѕРґРµР»Рё Chat РґР»СЏ РѕРїСЂРµРґРµР»РµРЅРёСЏ СЃС‚Р°С‚СѓСЃР° С‡Р°С‚Р°

using System.Text.Json.Serialization;

namespace Max.Bot.Types.Enums;

/// <summary>
/// Represents the status of a chat.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatStatus
{
    /// <summary>
    /// Bot is an active participant in the chat.
    /// </summary>
    Active,

    /// <summary>
    /// Bot was removed from the chat.
    /// </summary>
    Removed,

    /// <summary>
    /// Bot left the chat.
    /// </summary>
    Left,

    /// <summary>
    /// Chat was closed.
    /// </summary>
    Closed
}

