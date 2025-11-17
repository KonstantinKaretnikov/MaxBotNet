// рџ“Ѓ [ResponseTests] - РўРµСЃС‚С‹ РґР»СЏ Response<T> РјРѕРґРµР»Рё
// рџЋЇ Core function: РўРµСЃС‚РёСЂРѕРІР°РЅРёРµ СЃРµСЂРёР°Р»РёР·Р°С†РёРё/РґРµСЃРµСЂРёР°Р»РёР·Р°С†РёРё Response<T>
// рџ”— Key dependencies: Max.Bot.Types, Max.Bot.Networking, FluentAssertions, xUnit
// рџ’Ў Usage: Unit С‚РµСЃС‚С‹ РґР»СЏ РїСЂРѕРІРµСЂРєРё РєРѕСЂСЂРµРєС‚РЅРѕСЃС‚Рё СЂР°Р±РѕС‚С‹ Response<T>

using FluentAssertions;
using Max.Bot.Networking;
using Max.Bot.Types;
using Xunit;

namespace Max.Bot.Tests.Unit.Types;

public class ResponseTests
{
    [Fact]
    public void Deserialize_ShouldDeserializeSuccessfulResponse()
    {
        // Arrange
        var json = """{"ok":true,"result":{"id":123,"name":"Test"}}""";

        // Act
        var result = MaxJsonSerializer.Deserialize<Response<TestData>>(json);

        // Assert
        result.Should().NotBeNull();
        result!.Ok.Should().BeTrue();
        result.Result.Should().NotBeNull();
        result.Result!.Id.Should().Be(123);
        result.Result.Name.Should().Be("Test");
    }

    [Fact]
    public void Deserialize_ShouldDeserializeResponseWithNullResult()
    {
        // Arrange
        var json = """{"ok":true,"result":null}""";

        // Act
        var result = MaxJsonSerializer.Deserialize<Response<TestData>>(json);

        // Assert
        result.Should().NotBeNull();
        result!.Ok.Should().BeTrue();
        result.Result.Should().BeNull();
    }

    [Fact]
    public void Serialize_ShouldSerializeSuccessfulResponse()
    {
        // Arrange
        var response = new Response<TestData>
        {
            Ok = true,
            Result = new TestData { Id = 123, Name = "Test" }
        };

        // Act
        var json = MaxJsonSerializer.Serialize(response);

        // Assert
        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("\"ok\":true");
        json.Should().Contain("\"result\"");
        json.Should().Contain("\"id\":123");
        json.Should().Contain("\"name\":\"Test\"");
    }

    [Fact]
    public void Serialize_ShouldNotIncludeNullResult()
    {
        // Arrange
        var response = new Response<TestData>
        {
            Ok = true,
            Result = null
        };

        // Act
        var json = MaxJsonSerializer.Serialize(response);

        // Assert
        json.Should().NotBeNullOrEmpty();
        json.Should().Contain("\"ok\":true");
        json.Should().NotContain("\"result\"");
    }

    private class TestData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

