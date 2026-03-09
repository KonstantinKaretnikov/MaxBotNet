using System.Text.Json;
using FluentAssertions;
using Max.Bot.Types.Converters;
using Max.Bot.Types.Enums;
using Xunit;

namespace Max.Bot.Tests.Unit.Types.Converters;

/// <summary>
/// Unit tests for <see cref="UpdateTypeJsonConverter"/>.
/// </summary>
public class UpdateTypeJsonConverterTests
{
    private readonly JsonSerializerOptions _options;

    public UpdateTypeJsonConverterTests()
    {
        _options = new JsonSerializerOptions
        {
            Converters = { new UpdateTypeJsonConverter() }
        };
    }

    [Theory]
    [InlineData(UpdateType.Unknown, "\"unknown\"")]
    [InlineData(UpdateType.MessageCreated, "\"message_created\"")]
    [InlineData(UpdateType.MessageCallback, "\"message_callback\"")]
    [InlineData(UpdateType.MessageEdited, "\"message_edited\"")]
    [InlineData(UpdateType.MessageRemoved, "\"message_removed\"")]
    [InlineData(UpdateType.BotAdded, "\"bot_added\"")]
    [InlineData(UpdateType.BotRemoved, "\"bot_removed\"")]
    [InlineData(UpdateType.BotStarted, "\"bot_started\"")]
    [InlineData(UpdateType.BotStopped, "\"bot_stopped\"")]
    [InlineData(UpdateType.DialogMuted, "\"dialog_muted\"")]
    [InlineData(UpdateType.DialogUnmuted, "\"dialog_unmuted\"")]
    [InlineData(UpdateType.DialogCleared, "\"dialog_cleared\"")]
    [InlineData(UpdateType.DialogRemoved, "\"dialog_removed\"")]
    [InlineData(UpdateType.UserAdded, "\"user_added\"")]
    [InlineData(UpdateType.UserRemoved, "\"user_removed\"")]
    [InlineData(UpdateType.ChatTitleChanged, "\"chat_title_changed\"")]
    [InlineData(UpdateType.MessageChatCreated, "\"message_chat_created\"")]
    public void Write_ShouldSerializeEnumToString(UpdateType updateType, string expectedJson)
    {
        // Act
        var json = JsonSerializer.Serialize(updateType, _options);

        // Assert
        json.Should().Be(expectedJson);
    }

    [Theory]
    [InlineData("\"message_created\"", UpdateType.MessageCreated)]
    [InlineData("\"message_callback\"", UpdateType.MessageCallback)]
    [InlineData("\"message_edited\"", UpdateType.MessageEdited)]
    [InlineData("\"message_removed\"", UpdateType.MessageRemoved)]
    [InlineData("\"bot_added\"", UpdateType.BotAdded)]
    [InlineData("\"bot_removed\"", UpdateType.BotRemoved)]
    [InlineData("\"bot_started\"", UpdateType.BotStarted)]
    [InlineData("\"bot_stopped\"", UpdateType.BotStopped)]
    [InlineData("\"dialog_muted\"", UpdateType.DialogMuted)]
    [InlineData("\"dialog_unmuted\"", UpdateType.DialogUnmuted)]
    [InlineData("\"dialog_cleared\"", UpdateType.DialogCleared)]
    [InlineData("\"dialog_removed\"", UpdateType.DialogRemoved)]
    [InlineData("\"user_added\"", UpdateType.UserAdded)]
    [InlineData("\"user_removed\"", UpdateType.UserRemoved)]
    [InlineData("\"chat_title_changed\"", UpdateType.ChatTitleChanged)]
    [InlineData("\"message_chat_created\"", UpdateType.MessageChatCreated)]
    [InlineData("\"unknown\"", UpdateType.Unknown)]
    [InlineData("\"invalid_type\"", UpdateType.Unknown)]
    [InlineData("\"\"", UpdateType.Unknown)]
    [InlineData("null", UpdateType.Unknown)]
    [InlineData("0", UpdateType.Unknown)]
    [InlineData("1", UpdateType.MessageCreated)]
    [InlineData("2", UpdateType.MessageCallback)]
    [InlineData("16", UpdateType.MessageChatCreated)]
    public void Read_ShouldDeserializeStringToEnum(string json, UpdateType expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<UpdateType>(json, _options);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void RoundTrip_ShouldPreserveValue()
    {
        // Arrange
        var original = UpdateType.MessageCreated;

        // Act
        var json = JsonSerializer.Serialize(original, _options);
        var deserialized = JsonSerializer.Deserialize<UpdateType>(json, _options);

        // Assert
        deserialized.Should().Be(original);
    }
}
