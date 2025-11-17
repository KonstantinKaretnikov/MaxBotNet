// рџ“Ѓ [UpdateType] - РўРёРї РѕР±РЅРѕРІР»РµРЅРёСЏ РІ Max Messenger
// рџЋЇ Core function: РџРµСЂРµС‡РёСЃР»РµРЅРёРµ С‚РёРїРѕРІ РѕР±РЅРѕРІР»РµРЅРёР№ (message, callback_query)
// рџ”— Key dependencies: System.Text.Json.Serialization
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ РјРѕРґРµР»Рё Update РґР»СЏ РѕРїСЂРµРґРµР»РµРЅРёСЏ С‚РёРїР° РѕР±РЅРѕРІР»РµРЅРёСЏ

using System.Text.Json.Serialization;

namespace Max.Bot.Types.Enums;

/// <summary>
/// Represents the type of an update.
/// </summary>
public enum UpdateType
{
    /// <summary>
    /// New message update.
    /// </summary>
    Message,

    /// <summary>
    /// Callback query update.
    /// </summary>
    CallbackQuery
}

