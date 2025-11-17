// рџ“Ѓ [AddChatMembersRequest] - Р—Р°РїСЂРѕСЃ РЅР° РґРѕР±Р°РІР»РµРЅРёРµ СѓС‡Р°СЃС‚РЅРёРєРѕРІ РІ С‡Р°С‚
// рџЋЇ Core function: РџСЂРµРґСЃС‚Р°РІР»СЏРµС‚ Р·Р°РїСЂРѕСЃ РґР»СЏ РґРѕР±Р°РІР»РµРЅРёСЏ СѓС‡Р°СЃС‚РЅРёРєРѕРІ РІ С‡Р°С‚
// рџ”— Key dependencies: System.Text.Json.Serialization, System.ComponentModel.DataAnnotations
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ ChatsApi.AddChatMembersAsync РґР»СЏ РґРѕР±Р°РІР»РµРЅРёСЏ СѓС‡Р°СЃС‚РЅРёРєРѕРІ

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Max.Bot.Types.Requests;

/// <summary>
/// Represents a request to add members to a chat.
/// </summary>
public class AddChatMembersRequest
{
    /// <summary>
    /// Gets or sets the array of user IDs to add to the chat.
    /// </summary>
    /// <value>
    /// The array of user IDs. Must not be null or empty.
    /// </value>
    [Required(ErrorMessage = "User IDs array is required.")]
    [MinLength(1, ErrorMessage = "At least one user ID must be provided.")]
    [JsonPropertyName("user_ids")]
    public long[] UserIds { get; set; } = Array.Empty<long>();
}




