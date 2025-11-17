// рџ“Ѓ [UpdateTypeTests] - РўРµСЃС‚С‹ РґР»СЏ UpdateType enum
// рџЋЇ Core function: РўРµСЃС‚РёСЂРѕРІР°РЅРёРµ СЃРµСЂРёР°Р»РёР·Р°С†РёРё/РґРµСЃРµСЂРёР°Р»РёР·Р°С†РёРё UpdateType enum
// рџ”— Key dependencies: Max.Bot.Types.Enums, FluentAssertions, xUnit
// рџ’Ў Usage: Unit С‚РµСЃС‚С‹ РґР»СЏ РїСЂРѕРІРµСЂРєРё РєРѕСЂСЂРµРєС‚РЅРѕСЃС‚Рё СЂР°Р±РѕС‚С‹ UpdateType enum

using FluentAssertions;
using Max.Bot.Types.Enums;
using Max.Bot.Networking;
using Xunit;

namespace Max.Bot.Tests.Unit.Types.Enums;

public class UpdateTypeTests
{
    [Theory]
    [InlineData("message", UpdateType.Message)]
    [InlineData("callback_query", UpdateType.CallbackQuery)]
    public void Deserialize_ShouldParseJsonString(string jsonValue, UpdateType expected)
    {
        // Arrange
        var json = $"\"{jsonValue}\"";

        // Act
        var result = MaxJsonSerializer.Deserialize<UpdateType>(json);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(UpdateType.Message, "message")]
    [InlineData(UpdateType.CallbackQuery, "callback_query")]
    public void Serialize_ShouldConvertToJsonString(UpdateType value, string expectedJsonValue)
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
        var values = Enum.GetValues<UpdateType>();

        // Act & Assert
        foreach (var value in values)
        {
            var json = MaxJsonSerializer.Serialize(value);
            json.Should().NotBeNullOrEmpty();
            json.Should().StartWith("\"").And.EndWith("\"");
        }
    }
}

