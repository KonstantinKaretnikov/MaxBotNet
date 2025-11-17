// рџ“Ѓ [ChatType] - РўРёРї С‡Р°С‚Р° РІ Max Messenger
// рџЋЇ Core function: РџРµСЂРµС‡РёСЃР»РµРЅРёРµ С‚РёРїРѕРІ С‡Р°С‚РѕРІ (private, group, channel)
// рџ”— Key dependencies: System.Text.Json.Serialization
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ РјРѕРґРµР»Рё Chat РґР»СЏ РѕРїСЂРµРґРµР»РµРЅРёСЏ С‚РёРїР° С‡Р°С‚Р°

using System.Text.Json.Serialization;

namespace Max.Bot.Types.Enums;

/// <summary>
/// Represents the type of a chat.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatType
{
    /// <summary>
    /// Private chat with a user.
    /// </summary>
    Private,

    /// <summary>
    /// Group chat.
    /// </summary>
    Group,

    /// <summary>
    /// Channel.
    /// </summary>
    Channel
}

