// рџ“Ѓ [MessageType] - РўРёРї СЃРѕРѕР±С‰РµРЅРёСЏ РІ Max Messenger
// рџЋЇ Core function: РџРµСЂРµС‡РёСЃР»РµРЅРёРµ С‚РёРїРѕРІ СЃРѕРѕР±С‰РµРЅРёР№ (text, image, file)
// рџ”— Key dependencies: System.Text.Json.Serialization
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ РјРѕРґРµР»Рё Message РґР»СЏ РѕРїСЂРµРґРµР»РµРЅРёСЏ С‚РёРїР° СЃРѕРѕР±С‰РµРЅРёСЏ

using System.Text.Json.Serialization;

namespace Max.Bot.Types.Enums;

/// <summary>
/// Represents the type of a message.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MessageType
{
    /// <summary>
    /// Text message.
    /// </summary>
    Text,

    /// <summary>
    /// Image message.
    /// </summary>
    Image,

    /// <summary>
    /// File message.
    /// </summary>
    File
}

