// рџ“Ѓ [AddChatAdminRequest] - Р—Р°РїСЂРѕСЃ РЅР° РЅР°Р·РЅР°С‡РµРЅРёРµ Р°РґРјРёРЅРёСЃС‚СЂР°С‚РѕСЂР° С‡Р°С‚Р°
// рџЋЇ Core function: РџСЂРµРґСЃС‚Р°РІР»СЏРµС‚ Р·Р°РїСЂРѕСЃ РґР»СЏ РЅР°Р·РЅР°С‡РµРЅРёСЏ Р°РґРјРёРЅРёСЃС‚СЂР°С‚РѕСЂР° С‡Р°С‚Р°
// рџ”— Key dependencies: System.Text.Json.Serialization, System.ComponentModel.DataAnnotations
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ ChatsApi.AddChatAdminAsync РґР»СЏ РЅР°Р·РЅР°С‡РµРЅРёСЏ Р°РґРјРёРЅРёСЃС‚СЂР°С‚РѕСЂР°

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Max.Bot.Types.Requests;

/// <summary>
/// Represents a request to add an admin to a chat.
/// </summary>
public class AddChatAdminRequest
{
    /// <summary>
    /// Gets or sets the user ID to make an admin.
    /// </summary>
    /// <value>
    /// The user ID. Must be greater than zero.
    /// </value>
    [Range(1, long.MaxValue, ErrorMessage = "User ID must be greater than zero.")]
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}




