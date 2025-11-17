// 📁 [SampleBotsTests] - Tests for example bots
// 🎯 Core function: Validates wiring of sample bots without hitting real API
// 🔗 Key dependencies: Max.Bot.Examples, Moq, xUnit
// 💡 Usage: Guards against regressions in documentation samples

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Max.Bot.Api;
using Max.Bot.Configuration;
using Max.Bot.Examples;
using Max.Bot.Examples.Samples;
using Max.Bot.Polling;
using Max.Bot.Types;
using Max.Bot.Types.Enums;
using Max.Bot.Types.Requests;
using Moq;
using File = System.IO.File;

namespace Max.Bot.Tests.Examples;

/// <summary>
/// Ensures the sample bots keep compiling and interact with the API surface as expected.
/// </summary>
public class SampleBotsTests
{
    [Fact]
    public void SampleRegistry_ShouldExposeAllSamples()
    {
        SampleRegistry.AvailableSamples.Should().BeEquivalentTo(new[] { "echo", "commands", "keyboard", "files" });
    }

    [Fact]
    public async Task EchoBotSample_ShouldEchoMessages()
    {
        var (runtime, context, apiMock, messagesMock) = CreateRuntime();
        var sample = new EchoBotSample();

        using var cts = new CancellationTokenSource();
        var runTask = sample.RunAsync(context, cts.Token);
        var handler = await runtime.WaitForHandlerAsync();

        var update = CreateMessageUpdate("Hello world");
        await handler.HandleMessageAsync(CreateUpdateContext(apiMock.Object, update), CancellationToken.None);

        messagesMock.Verify(m => m.SendMessageAsync(update.Message!.Chat!.Id, "Echo: Hello world", It.IsAny<CancellationToken>()), Times.Once);

        cts.Cancel();
        await runTask.WaitAsync(TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task CommandBotSample_ShouldHandleCommands()
    {
        var (runtime, context, apiMock, messagesMock) = CreateRuntime();
        var chatsMock = new Mock<IChatsApi>();
        chatsMock.Setup(c => c.GetChatAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Chat { Id = 42, Title = "QA" });
        apiMock.SetupGet(x => x.Chats).Returns(chatsMock.Object);

        var sample = new CommandBotSample();

        using var cts = new CancellationTokenSource();
        var runTask = sample.RunAsync(context, cts.Token);
        var handler = await runtime.WaitForHandlerAsync();

        await handler.HandleMessageAsync(CreateUpdateContext(apiMock.Object, CreateMessageUpdate("/start")), CancellationToken.None);
        await handler.HandleMessageAsync(CreateUpdateContext(apiMock.Object, CreateMessageUpdate("/stats")), CancellationToken.None);

        messagesMock.Verify(m => m.SendMessageAsync(It.IsAny<long>(), It.Is<string>(text => text.Contains("Welcome")), It.IsAny<CancellationToken>()), Times.Once);
        messagesMock.Verify(m => m.SendMessageAsync(It.IsAny<long>(), It.Is<string>(text => text.Contains("QA")), It.IsAny<CancellationToken>()), Times.Once);

        cts.Cancel();
        await runTask.WaitAsync(TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task KeyboardBotSample_ShouldSendKeyboardAndAnswerCallbacks()
    {
        var (runtime, context, apiMock, messagesMock) = CreateRuntime();
        var answersMock = new Mock<IMessagesApi>();

        messagesMock.Setup(m => m.SendMessageAsync(
                It.IsAny<SendMessageRequest>(),
                It.IsAny<long?>(),
                It.IsAny<long?>(),
                It.IsAny<bool?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Message());
        apiMock.Setup(x => x.Messages).Returns(messagesMock.Object);

        var sample = new KeyboardBotSample();

        using var cts = new CancellationTokenSource();
        var runTask = sample.RunAsync(context, cts.Token);
        var handler = await runtime.WaitForHandlerAsync();

        await handler.HandleMessageAsync(CreateUpdateContext(apiMock.Object, CreateMessageUpdate("/buttons")), CancellationToken.None);
        messagesMock.Verify(m => m.SendMessageAsync(
            It.Is<SendMessageRequest>(request => request.Attachments != null && request.Attachments.Length > 0),
            1337,
            null,
            null,
            It.IsAny<CancellationToken>()), Times.Once);

        var callbackUpdate = new Update
        {
            UpdateId = 2,
            Type = UpdateType.CallbackQuery,
            CallbackQuery = new CallbackQuery
            {
                Id = "cb-1",
                Data = "vote:approve"
            }
        };

        messagesMock.Setup(m => m.AnswerCallbackQueryAsync(It.IsAny<string>(), It.IsAny<AnswerCallbackQueryRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response());

        await handler.HandleCallbackQueryAsync(CreateUpdateContext(apiMock.Object, callbackUpdate), CancellationToken.None);
        messagesMock.Verify(m => m.AnswerCallbackQueryAsync("cb-1", It.IsAny<AnswerCallbackQueryRequest>(), It.IsAny<CancellationToken>()), Times.Once);

        cts.Cancel();
        await runTask.WaitAsync(TimeSpan.FromSeconds(2));
    }

    [Fact]
    public async Task FileBotSample_ShouldUploadAttachments()
    {
        using var tempFile = new TempFile();
        await File.WriteAllTextAsync(tempFile.Path, "payload");

        var (runtime, context, apiMock, _) = CreateRuntime(uploadFilePath: tempFile.Path);
        var filesMock = new Mock<IFilesApi>();
        filesMock.Setup(f => f.UploadFileAsync(UploadType.File, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UploadResponse { Url = "https://upload" });
        filesMock.Setup(f => f.UploadFileDataAsync("https://upload", It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new { token = "file-token" });
        apiMock.SetupGet(x => x.Files).Returns(filesMock.Object);

        var messagesMock = new Mock<IMessagesApi>();
        messagesMock.Setup(m => m.SendMessageWithAttachmentAsync(It.IsAny<AttachmentRequest>(), It.IsAny<long?>(), It.IsAny<long?>(), It.IsAny<string?>(), It.IsAny<bool?>(), It.IsAny<bool?>(), It.IsAny<TextFormat?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Message());
        apiMock.SetupGet(x => x.Messages).Returns(messagesMock.Object);

        var sample = new FileBotSample();

        using var cts = new CancellationTokenSource();
        var runTask = sample.RunAsync(context, cts.Token);
        var handler = await runtime.WaitForHandlerAsync();

        await handler.HandleMessageAsync(CreateUpdateContext(apiMock.Object, CreateMessageUpdate("/file")), CancellationToken.None);

        filesMock.Verify(f => f.UploadFileAsync(UploadType.File, It.IsAny<CancellationToken>()), Times.Once);
        filesMock.Verify(f => f.UploadFileDataAsync("https://upload", It.IsAny<Stream>(), Path.GetFileName(tempFile.Path), It.IsAny<CancellationToken>()), Times.Once);
        messagesMock.Verify(m => m.SendMessageWithAttachmentAsync(It.IsAny<AttachmentRequest>(), 1337, null, It.IsAny<string?>(), It.IsAny<bool?>(), It.IsAny<bool?>(), It.IsAny<TextFormat?>(), It.IsAny<CancellationToken>()), Times.Once);

        cts.Cancel();
        await runTask.WaitAsync(TimeSpan.FromSeconds(2));
    }

    private static (TestSampleRuntime runtime, SampleExecutionContext context, Mock<IMaxBotApi> apiMock, Mock<IMessagesApi> messagesMock) CreateRuntime(string? uploadFilePath = null)
    {
        var apiMock = new Mock<IMaxBotApi>();
        var messagesMock = new Mock<IMessagesApi>();
        apiMock.SetupGet(x => x.Messages).Returns(messagesMock.Object);
        apiMock.SetupGet(x => x.Bot).Returns(Mock.Of<IBotApi>());
        apiMock.SetupGet(x => x.Chats).Returns(Mock.Of<IChatsApi>());
        apiMock.SetupGet(x => x.Users).Returns(Mock.Of<IUsersApi>());
        apiMock.SetupGet(x => x.Files).Returns(Mock.Of<IFilesApi>());
        apiMock.SetupGet(x => x.Subscriptions).Returns(Mock.Of<ISubscriptionsApi>());

        var runtime = new TestSampleRuntime(apiMock.Object);
        var settings = new SampleSettings("TEST_TOKEN", 1337, uploadFilePath);
        var context = new SampleExecutionContext(runtime, settings, TextWriter.Null, TextWriter.Null);

        return (runtime, context, apiMock, messagesMock);
    }

    private static Update CreateMessageUpdate(string text)
    {
        return new Update
        {
            UpdateId = 1,
            Type = UpdateType.Message,
            Message = new Message
            {
                Chat = new Chat { Id = 1337, Title = "SampleChat" },
                Body = new MessageBody { Text = text },
                Text = text
            }
        };
    }

    private static UpdateContext CreateUpdateContext(IMaxBotApi api, Update update)
    {
        return new UpdateContext(update, api, new MaxBotOptions { Token = "TEST_TOKEN" });
    }

    private sealed class TestSampleRuntime : ISampleRuntime
    {
        private readonly TaskCompletionSource<IUpdateHandler> _handlerSource = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TestSampleRuntime(IMaxBotApi api)
        {
            Api = api;
        }

        public IMaxBotApi Api { get; }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        public Task StartPollingAsync(IUpdateHandler handler, CancellationToken cancellationToken)
        {
            _handlerSource.TrySetResult(handler);
            return Task.CompletedTask;
        }

        public Task StopPollingAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task<IUpdateHandler> WaitForHandlerAsync() => _handlerSource.Task;
    }

    private sealed class TempFile : IDisposable
    {
        public TempFile()
        {
            Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"max-bot-{Guid.NewGuid():N}.tmp");
            System.IO.File.WriteAllText(Path, string.Empty);
        }

        public string Path { get; }

        public void Dispose()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
        }
    }
}

