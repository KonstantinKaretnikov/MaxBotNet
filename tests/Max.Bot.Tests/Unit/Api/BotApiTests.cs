// рџ“Ѓ [BotApiTests] - РўРµСЃС‚С‹ РґР»СЏ BotApi
// рџЋЇ Core function: РўРµСЃС‚РёСЂРѕРІР°РЅРёРµ РјРµС‚РѕРґРѕРІ BotApi (GetMeAsync, GetBotInfoAsync)
// рџ”— Key dependencies: Max.Bot.Api, Max.Bot.Configuration, Max.Bot.Networking, Max.Bot.Types, Moq, FluentAssertions, xUnit
// рџ’Ў Usage: Unit С‚РµСЃС‚С‹ РґР»СЏ РїСЂРѕРІРµСЂРєРё РєРѕСЂСЂРµРєС‚РЅРѕСЃС‚Рё СЂР°Р±РѕС‚С‹ BotApi

using FluentAssertions;
using Max.Bot.Api;
using Max.Bot.Configuration;
using Max.Bot.Exceptions;
using Max.Bot.Networking;
using Max.Bot.Types;
using Moq;
using System.Net;
using System.Net.Http;
using Xunit;

namespace Max.Bot.Tests.Unit.Api;

public class BotApiTests
{
    private readonly Mock<IMaxHttpClient> _mockHttpClient;
    private readonly MaxBotOptions _options;

    public BotApiTests()
    {
        _mockHttpClient = new Mock<IMaxHttpClient>();
        _options = new MaxBotOptions
        {
            Token = "test-token-123",
            BaseUrl = "https://api.max.ru/bot"
        };
    }

    [Fact]
    public async Task GetMeAsync_ShouldReturnUser_WhenRequestSucceeds()
    {
        // Arrange
        var expectedUser = new User
        {
            Id = 123456,
            Username = "test_bot",
            FirstName = "Test",
            IsBot = true
        };

        var response = new Response<User>
        {
            Ok = true,
            Result = expectedUser
        };

        _mockHttpClient
            .Setup(x => x.SendAsync<Response<User>>(
                It.Is<MaxApiRequest>(req =>
                    req.Method == HttpMethod.Get &&
                    req.Endpoint == "/test-token-123/me" &&
                    req.Headers != null &&
                    req.Headers.ContainsKey("Authorization") &&
                    req.Headers["Authorization"] == "Bearer test-token-123"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var botApi = new BotApi(_mockHttpClient.Object, _options);

        // Act
        var result = await botApi.GetMeAsync();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedUser.Id);
        result.Username.Should().Be(expectedUser.Username);
        result.FirstName.Should().Be(expectedUser.FirstName);
        result.IsBot.Should().BeTrue();

        _mockHttpClient.Verify(
            x => x.SendAsync<Response<User>>(
                It.IsAny<MaxApiRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetMeAsync_ShouldThrowMaxApiException_WhenResponseIsNotOk()
    {
        // Arrange
        var response = new Response<User>
        {
            Ok = false,
            Result = null
        };

        _mockHttpClient
            .Setup(x => x.SendAsync<Response<User>>(
                It.IsAny<MaxApiRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var botApi = new BotApi(_mockHttpClient.Object, _options);

        // Act
        var act = async () => await botApi.GetMeAsync();

        // Assert
        await act.Should().ThrowAsync<MaxApiException>();
    }

    [Fact]
    public async Task GetBotInfoAsync_ShouldReturnUser_WhenRequestSucceeds()
    {
        // Arrange
        var expectedUser = new User
        {
            Id = 123456,
            Username = "test_bot",
            FirstName = "Test",
            IsBot = true
        };

        var response = new Response<User>
        {
            Ok = true,
            Result = expectedUser
        };

        _mockHttpClient
            .Setup(x => x.SendAsync<Response<User>>(
                It.Is<MaxApiRequest>(req =>
                    req.Method == HttpMethod.Get &&
                    req.Endpoint == "/test-token-123/bot/info" &&
                    req.Headers != null &&
                    req.Headers.ContainsKey("Authorization") &&
                    req.Headers["Authorization"] == "Bearer test-token-123"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var botApi = new BotApi(_mockHttpClient.Object, _options);

        // Act
        var result = await botApi.GetBotInfoAsync();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(expectedUser.Id);
        result.Username.Should().Be(expectedUser.Username);
        result.FirstName.Should().Be(expectedUser.FirstName);
        result.IsBot.Should().BeTrue();

        _mockHttpClient.Verify(
            x => x.SendAsync<Response<User>>(
                It.IsAny<MaxApiRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetBotInfoAsync_ShouldThrowMaxApiException_WhenResponseIsNotOk()
    {
        // Arrange
        var response = new Response<User>
        {
            Ok = false,
            Result = null
        };

        _mockHttpClient
            .Setup(x => x.SendAsync<Response<User>>(
                It.IsAny<MaxApiRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var botApi = new BotApi(_mockHttpClient.Object, _options);

        // Act
        var act = async () => await botApi.GetBotInfoAsync();

        // Assert
        await act.Should().ThrowAsync<MaxApiException>();
    }

    [Fact]
    public async Task GetMeAsync_ShouldPropagateMaxUnauthorizedException_WhenAuthenticationFails()
    {
        // Arrange
        _mockHttpClient
            .Setup(x => x.SendAsync<Response<User>>(
                It.IsAny<MaxApiRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new MaxUnauthorizedException("Unauthorized", "INVALID_TOKEN", HttpStatusCode.Unauthorized));

        var botApi = new BotApi(_mockHttpClient.Object, _options);

        // Act
        var act = async () => await botApi.GetMeAsync();

        // Assert
        await act.Should().ThrowAsync<MaxUnauthorizedException>();
    }

    [Fact]
    public async Task GetBotInfoAsync_ShouldPropagateMaxUnauthorizedException_WhenAuthenticationFails()
    {
        // Arrange
        _mockHttpClient
            .Setup(x => x.SendAsync<Response<User>>(
                It.IsAny<MaxApiRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new MaxUnauthorizedException("Unauthorized", "INVALID_TOKEN", HttpStatusCode.Unauthorized));

        var botApi = new BotApi(_mockHttpClient.Object, _options);

        // Act
        var act = async () => await botApi.GetBotInfoAsync();

        // Assert
        await act.Should().ThrowAsync<MaxUnauthorizedException>();
    }
}

