// рџ“Ѓ [MessageTypeTests] - РўРµСЃС‚С‹ РґР»СЏ MessageType enum
// рџЋЇ Core function: РўРµСЃС‚РёСЂРѕРІР°РЅРёРµ СЃРµСЂРёР°Р»РёР·Р°С†РёРё/РґРµСЃРµСЂРёР°Р»РёР·Р°С†РёРё MessageType enum
// рџ”— Key dependencies: Max.Bot.Types.Enums, FluentAssertions, xUnit
// рџ’Ў Usage: Unit С‚РµСЃС‚С‹ РґР»СЏ РїСЂРѕРІРµСЂРєРё РєРѕСЂСЂРµРєС‚РЅРѕСЃС‚Рё СЂР°Р±РѕС‚С‹ MessageType enum

using FluentAssertions;
using Max.Bot.Types.Enums;
using Max.Bot.Networking;
using Xunit;

namespace Max.Bot.Tests.Unit.Types.Enums;

public class MessageTypeTests
{
    [Theory]
    [InlineData("text", MessageType.Text)]
    [InlineData("image", MessageType.Image)]
    [InlineData("file", MessageType.File)]
    public void Deserialize_ShouldParseJsonString(string jsonValue, MessageType expected)
    {
        // Arrange
        var json = $"\"{jsonValue}\"";

        // Act
        var result = MaxJsonSerializer.Deserialize<MessageType>(json);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(MessageType.Text, "text")]
    [InlineData(MessageType.Image, "image")]
    [InlineData(MessageType.File, "file")]
    public void Serialize_ShouldConvertToJsonString(MessageType value, string expectedJsonValue)
    {
        // Arrange
        var expectedJson = $"\"{expectedJsonValue}\"";

        // Act
        var json = MaxJsonSerializer.Serialize(value);

        // Assert
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void Serialize_ShouldHandleAllValues()
    {
        // Arrange
        var values = Enum.GetValues<MessageType>();

        // Act & Assert
        foreach (var value in values)
        {
            var json = MaxJsonSerializer.Serialize(value);
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("\"").And.EndWith("\"");
        }
    }
}

