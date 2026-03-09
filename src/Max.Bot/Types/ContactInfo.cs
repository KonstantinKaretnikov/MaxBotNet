using System.Text.Json.Serialization;

namespace Max.Bot.Types;

/// <summary>
/// Represents a contact information.
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// Gets or sets the unique identifier of the contact.
    /// </summary>
    /// <value>The unique identifier of the contact.</value>
    [JsonPropertyName("user_id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the contact.
    /// </summary>
    /// <value>The username of the contact, or null if not available.</value>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the first name of the contact.
    /// </summary>
    /// <value>The first name of the contact, or null if not available.</value>
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the contact.
    /// </summary>
    /// <value>The last name of the contact, or null if not available.</value>
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the contact is a bot.
    /// </summary>
    /// <value>True if the user is a bot; otherwise, false.</value>
    [JsonPropertyName("is_bot")]
    public bool IsBot { get; set; }

    /// <summary>
    /// Gets or sets the last activity time of the contact (Unix timestamp in milliseconds).
    /// </summary>
    /// <value>The timestamp of the user's last activity.</value>
    [JsonPropertyName("last_activity_time")]
    public long? LastActivityTime { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the contact.
    /// Note: This field may not be directly returned by the API.
    /// Use <see cref="ContactHelpers.GetPhoneNumber"/> to extract from vcf_info if needed.
    /// </summary>
    /// <value>The phone number of the contact, or null if not available.</value>
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the full name of the contact.
    /// Note: This field may not be directly returned by the API.
    /// Use <see cref="ContactHelpers.GetFullName"/> to extract from vcf_info if needed.
    /// </summary>
    /// <value>The full name of the contact, or null if not available.</value>
    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the name of the contact (alternative to full_name).
    /// This is an alias for <see cref="FullName"/> to support different API response formats.
    /// </summary>
    /// <value>The name of the contact, or null if not available.</value>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name
    {
        get => FullName;
        set => FullName = value;
    }
}
