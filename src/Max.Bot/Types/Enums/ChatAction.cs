// рџ“Ѓ [ChatAction] - Enum РґР»СЏ РґРµР№СЃС‚РІРёР№ Р±РѕС‚Р° РІ С‡Р°С‚Рµ
// рџЋЇ Core function: РћРїСЂРµРґРµР»СЏРµС‚ РІРѕР·РјРѕР¶РЅС‹Рµ РґРµР№СЃС‚РІРёСЏ Р±РѕС‚Р° РІ С‡Р°С‚Рµ
// рџ”— Key dependencies: System.Text.Json.Serialization
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ РІ SendChatActionRequest РґР»СЏ РѕС‚РїСЂР°РІРєРё РґРµР№СЃС‚РІРёР№

using System.Text.Json.Serialization;

namespace Max.Bot.Types.Enums;

/// <summary>
/// Represents actions that can be sent by the bot in a chat.
/// </summary>
public enum ChatAction
{
    /// <summary>
    /// Bot is typing a message.
    /// </summary>
    [JsonPropertyName("typing_on")]
    TypingOn,

    /// <summary>
    /// Bot is sending a photo.
    /// </summary>
    [JsonPropertyName("sending_photo")]
    SendingPhoto,

    /// <summary>
    /// Bot is sending a video.
    /// </summary>
    [JsonPropertyName("sending_video")]
    SendingVideo,

    /// <summary>
    /// Bot is sending an audio file.
    /// </summary>
    [JsonPropertyName("sending_audio")]
    SendingAudio,

    /// <summary>
    /// Bot is sending a file.
    /// </summary>
    [JsonPropertyName("sending_file")]
    SendingFile,

    /// <summary>
    /// Bot marks messages as seen.
    /// </summary>
    [JsonPropertyName("mark_seen")]
    MarkSeen
}

