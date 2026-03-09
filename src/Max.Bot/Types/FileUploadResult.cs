using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Max.Bot.Types;

/// <summary>
/// Represents the result of a file upload.
/// Contains the token or file identifier for the uploaded content.
/// </summary>
public class FileUploadResult
{
    /// <summary>
    /// Gets or sets the unique token of the uploaded file.
    /// Used for audio, video, and general file attachments.
    /// </summary>
    /// <value>The file token.</value>
    [JsonPropertyName("token")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Token { get; set; }

    /// <summary>
    /// Gets or sets the file identifier.
    /// Used for audio, video, and general file attachments.
    /// </summary>
    /// <value>The file identifier.</value>
    [JsonPropertyName("file_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? FileId { get; set; }

    /// <summary>
    /// Gets or sets the photo tokens received after image upload.
    /// For images, the API returns a map of sizes (e.g., "orig", "thumb") to tokens.
    /// </summary>
    /// <value>The dictionary of photo size tokens.</value>
    [JsonPropertyName("photos")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, PhotoSizeToken>? Photos { get; set; }

    /// <summary>
    /// Gets or sets additional properties received in the response.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}

/// <summary>
/// Represents a token for a specific photo size.
/// </summary>
public class PhotoSizeToken
{
    /// <summary>
    /// Gets or sets the token for this photo size.
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
