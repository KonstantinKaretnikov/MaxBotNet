using System.Text.Json;
using FluentAssertions;
using Max.Bot.Networking;
using Max.Bot.Types;
using Max.Bot.Types.Enums;
using Max.Bot.Types.Requests;
using Xunit;

namespace Max.Bot.Tests.Unit.Types;

/// <summary>
/// Unit tests for webhook request/response serialization to ensure compliance with MAX API specification.
/// </summary>
public class WebhookSerializationTests
{
    [Fact]
    public void SetWebhookRequest_ShouldSerialize_WithSnakeCaseFields()
    {
        // Arrange
        var request = new SetWebhookRequest
        {
            Url = "https://example.com/webhook",
            UpdateTypes = new List<UpdateType> { UpdateType.MessageCreated, UpdateType.BotStarted },
            Secret = "my-secret-123"
        };

        // Act
        var json = MaxJsonSerializer.Serialize(request);

        // Assert
        json.Should().Contain("\"url\":\"https://example.com/webhook\"");
        json.Should().Contain("\"update_types\"");
        json.Should().Contain("\"message_created\"");
        json.Should().Contain("\"bot_started\"");
        json.Should().Contain("\"secret\":\"my-secret-123\"");
    }

    [Fact]
    public void SetWebhookRequest_ShouldDeserialize_FromSnakeCaseJson()
    {
        // Arrange
        var json = """{"url":"https://example.com/webhook","update_types":["message_created"],"secret":"my-secret"}""";

        // Act
        var request = MaxJsonSerializer.Deserialize<SetWebhookRequest>(json);

        // Assert
        request.Should().NotBeNull();
        request!.Url.Should().Be("https://example.com/webhook");
        request.UpdateTypes.Should().NotBeNull();
        request.UpdateTypes!.Should().Contain(UpdateType.MessageCreated);
        request.Secret.Should().Be("my-secret");
    }

    [Fact]
    public void DeleteWebhookRequest_ShouldSerialize_WithSnakeCaseFields()
    {
        // Arrange
        var request = new DeleteWebhookRequest
        {
            Url = "https://example.com/webhook"
        };

        // Act
        var json = MaxJsonSerializer.Serialize(request);

        // Assert
        json.Should().Contain("\"url\":\"https://example.com/webhook\"");
    }

    [Fact]
    public void DeleteWebhookRequest_ShouldDeserialize_FromSnakeCaseJson()
    {
        // Arrange
        var json = """{"url":"https://example.com/webhook"}""";

        // Act
        var request = MaxJsonSerializer.Deserialize<DeleteWebhookRequest>(json);

        // Assert
        request.Should().NotBeNull();
        request!.Url.Should().Be("https://example.com/webhook");
    }

    [Fact]
    public void Subscription_ShouldDeserialize_WithAllFields()
    {
        // Arrange
        var json = """{"url":"https://example.com/webhook","update_types":["message_created"],"secret":"my-secret","created_at":1704067200,"updated_at":1704153600}""";

        // Act
        var subscription = MaxJsonSerializer.Deserialize<Subscription>(json);

        // Assert
        subscription.Should().NotBeNull();
        subscription!.Url.Should().Be("https://example.com/webhook");
        subscription.UpdateTypes.Should().NotBeNull();
        subscription.UpdateTypes!.Should().Contain(UpdateType.MessageCreated);
        subscription.Secret.Should().Be("my-secret");
        subscription.CreatedAt.Should().NotBeNull();
        subscription.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Subscription_ShouldSerialize_WithSnakeCaseFields()
    {
        // Arrange
        var subscription = new Subscription
        {
            Url = "https://example.com/webhook",
            UpdateTypes = new List<UpdateType> { UpdateType.MessageCreated },
            Secret = "my-secret",
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
        };

        // Act
        var json = MaxJsonSerializer.Serialize(subscription);

        // Assert
        json.Should().Contain("\"url\":\"https://example.com/webhook\"");
        json.Should().Contain("\"update_types\"");
        json.Should().Contain("\"secret\":\"my-secret\"");
        json.Should().Contain("\"created_at\"");
        json.Should().Contain("\"updated_at\"");
    }

    [Fact]
    public void SetWebhookRequest_ShouldHandle_NullUpdateTypes()
    {
        // Arrange
        var request = new SetWebhookRequest
        {
            Url = "https://example.com/webhook",
            UpdateTypes = null,
            Secret = "my-secret"
        };

        // Act
        var json = MaxJsonSerializer.Serialize(request);

        // Assert
        json.Should().Contain("\"url\":\"https://example.com/webhook\"");
        json.Should().Contain("\"secret\":\"my-secret\"");
        // update_types should be omitted when null
        json.Should().NotContain("update_types");
    }

    [Fact]
    public void SetWebhookRequest_ShouldHandle_EmptyUpdateTypes()
    {
        // Arrange
        var request = new SetWebhookRequest
        {
            Url = "https://example.com/webhook",
            UpdateTypes = new List<UpdateType>(),
            Secret = "my-secret"
        };

        // Act
        var json = MaxJsonSerializer.Serialize(request);

        // Assert
        json.Should().Contain("\"url\":\"https://example.com/webhook\"");
        json.Should().Contain("\"update_types\":[]");
    }

    [Fact]
    public void Subscription_RoundTrip_AllUpdateTypes()
    {
        // Arrange
        var allUpdateTypes = Enum.GetValues<UpdateType>().Where(x => x != UpdateType.Unknown).ToList();
        var subscription = new Subscription
        {
            Url = "https://example.com/webhook",
            UpdateTypes = allUpdateTypes,
            Secret = "my-secret"
        };

        // Act
        var json = MaxJsonSerializer.Serialize(subscription);
        var deserialized = MaxJsonSerializer.Deserialize<Subscription>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.UpdateTypes.Should().HaveCount(allUpdateTypes.Count);
        deserialized.UpdateTypes!.Should().BeEquivalentTo(allUpdateTypes);
    }

    [Fact]
    public void SetWebhookRequest_RoundTrip_AllUpdateTypes()
    {
        // Arrange
        var allUpdateTypes = Enum.GetValues<UpdateType>().Where(x => x != UpdateType.Unknown).ToList();
        var request = new SetWebhookRequest
        {
            Url = "https://example.com/webhook",
            UpdateTypes = allUpdateTypes
        };

        // Act
        var json = MaxJsonSerializer.Serialize(request);
        var deserialized = MaxJsonSerializer.Deserialize<SetWebhookRequest>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized!.UpdateTypes.Should().HaveCount(allUpdateTypes.Count);
        deserialized.UpdateTypes!.Should().BeEquivalentTo(allUpdateTypes);
    }
}

