using FluentAssertions;
using Max.Bot.Types.Enums;
using Max.Bot.Types.Helpers;
using Xunit;

namespace Max.Bot.Tests.Unit.Types.Helpers;

/// <summary>
/// Unit tests for <see cref="UpdateTypeHelper"/>.
/// </summary>
public class UpdateTypeHelperTests
{
    #region ToStringValue Tests

    [Theory]
    [InlineData(UpdateType.MessageCreated, "message_created")]
    [InlineData(UpdateType.MessageCallback, "message_callback")]
    [InlineData(UpdateType.MessageEdited, "message_edited")]
    [InlineData(UpdateType.MessageRemoved, "message_removed")]
    [InlineData(UpdateType.BotAdded, "bot_added")]
    [InlineData(UpdateType.BotRemoved, "bot_removed")]
    [InlineData(UpdateType.BotStarted, "bot_started")]
    [InlineData(UpdateType.BotStopped, "bot_stopped")]
    [InlineData(UpdateType.DialogMuted, "dialog_muted")]
    [InlineData(UpdateType.DialogUnmuted, "dialog_unmuted")]
    [InlineData(UpdateType.DialogCleared, "dialog_cleared")]
    [InlineData(UpdateType.DialogRemoved, "dialog_removed")]
    [InlineData(UpdateType.UserAdded, "user_added")]
    [InlineData(UpdateType.UserRemoved, "user_removed")]
    [InlineData(UpdateType.ChatTitleChanged, "chat_title_changed")]
    [InlineData(UpdateType.MessageChatCreated, "message_chat_created")]
    [InlineData(UpdateType.Unknown, "unknown")]
    public void ToStringValue_ShouldConvertEnumToString(UpdateType updateType, string expected)
    {
        // Act
        var result = UpdateTypeHelper.ToStringValue(updateType);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region FromString Tests

    [Theory]
    [InlineData("message_created", UpdateType.MessageCreated)]
    [InlineData("message_callback", UpdateType.MessageCallback)]
    [InlineData("message_edited", UpdateType.MessageEdited)]
    [InlineData("message_removed", UpdateType.MessageRemoved)]
    [InlineData("bot_added", UpdateType.BotAdded)]
    [InlineData("bot_removed", UpdateType.BotRemoved)]
    [InlineData("bot_started", UpdateType.BotStarted)]
    [InlineData("bot_stopped", UpdateType.BotStopped)]
    [InlineData("dialog_muted", UpdateType.DialogMuted)]
    [InlineData("dialog_unmuted", UpdateType.DialogUnmuted)]
    [InlineData("dialog_cleared", UpdateType.DialogCleared)]
    [InlineData("dialog_removed", UpdateType.DialogRemoved)]
    [InlineData("user_added", UpdateType.UserAdded)]
    [InlineData("user_removed", UpdateType.UserRemoved)]
    [InlineData("chat_title_changed", UpdateType.ChatTitleChanged)]
    [InlineData("message_chat_created", UpdateType.MessageChatCreated)]
    [InlineData("unknown", UpdateType.Unknown)]
    [InlineData(null, UpdateType.Unknown)]
    [InlineData("", UpdateType.Unknown)]
    [InlineData("invalid_type", UpdateType.Unknown)]
    public void FromString_ShouldConvertStringToEnum(string? input, UpdateType expected)
    {
        // Act
        var result = UpdateTypeHelper.FromString(input);

        // Assert
        result.Should().Be(expected);
    }

    #endregion

    #region ToStringValues Tests

    [Fact]
    public void ToStringValues_ShouldConvertCollection()
    {
        // Arrange
        var updateTypes = new List<UpdateType>
        {
            UpdateType.MessageCreated,
            UpdateType.BotStarted,
            UpdateType.MessageCallback
        };

        // Act
        var result = UpdateTypeHelper.ToStringValues(updateTypes).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain("message_created");
        result.Should().Contain("bot_started");
        result.Should().Contain("message_callback");
    }

    [Fact]
    public void ToStringValues_ShouldReturnEmpty_WhenNull()
    {
        // Act
        var result = UpdateTypeHelper.ToStringValues(null!);

        // Assert
        result.Should().BeEmpty();
    }

    #endregion

    #region FromStrings Tests

    [Fact]
    public void FromStrings_ShouldConvertCollection()
    {
        // Arrange
        var strings = new List<string?> { "message_created", "bot_started", "message_callback" };

        // Act
        var result = UpdateTypeHelper.FromStrings(strings).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain(UpdateType.MessageCreated);
        result.Should().Contain(UpdateType.BotStarted);
        result.Should().Contain(UpdateType.MessageCallback);
    }

    [Fact]
    public void FromStrings_ShouldReturnEmpty_WhenNull()
    {
        // Act
        var result = UpdateTypeHelper.FromStrings(null!);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void FromStrings_ShouldHandleInvalidStrings()
    {
        // Arrange
        var strings = new List<string?> { "message_created", "invalid_type", null };

        // Act
        var result = UpdateTypeHelper.FromStrings(strings).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().Contain(UpdateType.MessageCreated);
        result.Should().Contain(UpdateType.Unknown);
    }

    #endregion
}
