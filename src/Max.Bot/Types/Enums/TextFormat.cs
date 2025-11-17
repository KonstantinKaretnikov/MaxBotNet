// рџ“Ѓ [TextFormat] - Р¤РѕСЂРјР°С‚ С‚РµРєСЃС‚Р° РІ Max Messenger
// рџЋЇ Core function: РџРµСЂРµС‡РёСЃР»РµРЅРёРµ С„РѕСЂРјР°С‚РѕРІ С‚РµРєСЃС‚Р° (markdown, html)
// рџ”— Key dependencies: System.Text.Json.Serialization
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ РјРѕРґРµР»СЏС… Р·Р°РїСЂРѕСЃРѕРІ РґР»СЏ С„РѕСЂРјР°С‚РёСЂРѕРІР°РЅРёСЏ С‚РµРєСЃС‚Р°

using System.Text.Json.Serialization;

namespace Max.Bot.Types.Enums;

/// <summary>
/// Represents the text format for message content.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TextFormat
{
    /// <summary>
    /// Markdown formatting.
    /// </summary>
    Markdown,

    /// <summary>
    /// HTML formatting.
    /// </summary>
    Html
}




