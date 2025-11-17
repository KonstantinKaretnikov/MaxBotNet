// рџ“Ѓ [IMaxHttpClientTests] - РўРµСЃС‚С‹ РґР»СЏ РёРЅС‚РµСЂС„РµР№СЃР° HTTP РєР»РёРµРЅС‚Р°
// рџЋЇ Core function: РџСЂРѕРІРµСЂРєР° СЃРёРіРЅР°С‚СѓСЂ РјРµС‚РѕРґРѕРІ РёРЅС‚РµСЂС„РµР№СЃР° Рё РІРѕР·РјРѕР¶РЅРѕСЃС‚СЊ СЃРѕР·РґР°РЅРёСЏ РјРѕРєРѕРІ
// рџ”— Key dependencies: Max.Bot.Networking, Moq, FluentAssertions, xUnit
// рџ’Ў Usage: Unit С‚РµСЃС‚С‹ РґР»СЏ РїСЂРѕРІРµСЂРєРё РєРѕСЂСЂРµРєС‚РЅРѕСЃС‚Рё РёРЅС‚РµСЂС„РµР№СЃР° IMaxHttpClient

using System.Net.Http;
using FluentAssertions;
using Max.Bot.Networking;
using Moq;
using Xunit;

namespace Max.Bot.Tests.Unit.Networking;

public class IMaxHttpClientTests
{
    [Fact]
    public void IMaxHttpClient_ShouldHaveTwoSendAsyncMethods()
    {
        // Arrange & Act
        var methods = typeof(IMaxHttpClient).GetMethods();

        // Assert
        methods.Should().HaveCount(2);
        methods.Should().Contain(m => m.Name == "SendAsync" && m.IsGenericMethod);
        methods.Should().Contain(m => m.Name == "SendAsync" && !m.IsGenericMethod);
    }

    [Fact]
    public void IMaxHttpClient_GenericSendAsync_ShouldReturnTask()
    {
        // Arrange
        var methods = typeof(IMaxHttpClient).GetMethods().Where(m => m.Name == "SendAsync" && m.IsGenericMethod).ToList();

        // Assert
        methods.Should().HaveCount(1);
        var method = methods[0];
        method.ReturnType.IsGenericType.Should().BeTrue();
        method.ReturnType.GetGenericTypeDefinition().Should().Be(typeof(Task<>));
    }

    [Fact]
    public void IMaxHttpClient_NonGenericSendAsync_ShouldReturnTask()
    {
        // Arrange
        var methods = typeof(IMaxHttpClient).GetMethods().Where(m => m.Name == "SendAsync" && !m.IsGenericMethod).ToList();

        // Assert
        methods.Should().HaveCount(1);
        methods[0].ReturnType.Should().Be(typeof(Task));
    }

    [Fact]
    public async Task IMaxHttpClient_ShouldBeMockable()
    {
        // Arrange
        var mockClient = new Mock<IMaxHttpClient>();
        var request = new MaxApiRequest
        {
            Method = HttpMethod.Get,
            Endpoint = "test"
        };

        // Act
        mockClient.Setup(x => x.SendAsync<string>(It.IsAny<MaxApiRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync("test response");

        // Assert
        mockClient.Should().NotBeNull();
        var result = await mockClient.Object.SendAsync<string>(request, default);
        result.Should().Be("test response");
    }

    [Fact]
    public void IMaxHttpClient_VoidSendAsync_ShouldBeMockable()
    {
        // Arrange
        var mockClient = new Mock<IMaxHttpClient>();
        var request = new MaxApiRequest
        {
            Method = HttpMethod.Post,
            Endpoint = "test"
        };

        // Act
        mockClient.Setup(x => x.SendAsync(It.IsAny<MaxApiRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Assert
        var act = async () => await mockClient.Object.SendAsync(request, default);
        act.Should().NotThrowAsync();
    }
}

