// рџ“Ѓ [SendChatActionRequest] - Р—Р°РїСЂРѕСЃ РЅР° РѕС‚РїСЂР°РІРєСѓ РґРµР№СЃС‚РІРёСЏ РІ С‡Р°С‚
// рџЋЇ Core function: РџСЂРµРґСЃС‚Р°РІР»СЏРµС‚ Р·Р°РїСЂРѕСЃ РґР»СЏ РѕС‚РїСЂР°РІРєРё РґРµР№СЃС‚РІРёСЏ Р±РѕС‚Р° РІ С‡Р°С‚
// рџ”— Key dependencies: System.Text.Json.Serialization, Max.Bot.Types.Enums
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ ChatsApi.SendChatActionAsync РґР»СЏ РѕС‚РїСЂР°РІРєРё РґРµР№СЃС‚РІРёР№

using System.Text.Json.Serialization;
using Max.Bot.Types.Enums;

namespace Max.Bot.Types.Requests;

/// <summary>
/// Represents a request to send a chat action.
/// </summary>
public class SendChatActionRequest
{
    /// <summary>
    /// Gets or sets the action to send.
    /// </summary>
    /// <value>The chat action to send.</value>
    [JsonPropertyName("action")]
    public ChatAction Action { get; set; }
}




