// рџ“Ѓ [UpdateHandlerExecutor] - Р’СЃРїРѕРјРѕРіР°С‚РµР»СЊРЅС‹Р№ РёСЃРїРѕР»РЅРёС‚РµР»СЊ РѕР±СЂР°Р±РѕС‚С‡РёРєРѕРІ
// рџЋЇ Core function: РРЅРєР°РїСЃСѓР»РёСЂСѓРµС‚ РІС‹Р·РѕРІ IUpdateHandler СЃ С‚Р°Р№РјР°СѓС‚РѕРј Рё Р»РѕРіРёСЂРѕРІР°РЅРёРµРј
// рџ”— Key dependencies: System.Threading.Tasks, Microsoft.Extensions.Logging, Max.Bot.Api, Max.Bot.Configuration, Max.Bot.Types, Max.Bot.Types.Enums
// рџ’Ў Usage: РСЃРїРѕР»СЊР·СѓРµС‚СЃСЏ UpdatePoller Рё WebhookController РґР»СЏ РµРґРёРЅРѕРѕР±СЂР°Р·РЅРѕР№ РѕР±СЂР°Р±РѕС‚РєРё

using System;
using System.Threading;
using System.Threading.Tasks;
using Max.Bot.Api;
using Max.Bot.Configuration;
using Max.Bot.Types;
using Max.Bot.Types.Enums;
using Microsoft.Extensions.Logging;

namespace Max.Bot.Polling;

internal static class UpdateHandlerExecutor
{
    public static async Task ExecuteAsync(
        Update update,
        IUpdateHandler handler,
        MaxBotOptions options,
        IMaxBotApi api,
        ILogger? logger,
        IServiceProvider? services,
        CancellationToken cancellationToken)
    {
        using var handlerCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        if (options.Handling.HandlerTimeout > TimeSpan.Zero)
        {
            handlerCts.CancelAfter(options.Handling.HandlerTimeout);
        }

        var context = new UpdateContext(update, api, options, logger, services);

        try
        {
            await handler.HandleUpdateAsync(context, handlerCts.Token).ConfigureAwait(false);

            switch (update.Type)
            {
                case UpdateType.Message:
                    await handler.HandleMessageAsync(context, handlerCts.Token).ConfigureAwait(false);
                    break;
                case UpdateType.CallbackQuery:
                    await handler.HandleCallbackQueryAsync(context, handlerCts.Token).ConfigureAwait(false);
                    break;
                default:
                    await handler.HandleUnknownUpdateAsync(context, handlerCts.Token).ConfigureAwait(false);
                    break;
            }
        }
        catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested && handlerCts.IsCancellationRequested)
        {
            if (options.Handling.PropagateHandlerExceptions)
            {
                throw;
            }

            logger?.LogWarning(ex, "Update handler timed out after {Timeout}. UpdateId={UpdateId}", options.Handling.HandlerTimeout, update.UpdateId);
        }
        catch (Exception ex)
        {
            if (options.Handling.PropagateHandlerExceptions)
            {
                throw;
            }

            logger?.LogError(ex, "Update handler threw an exception for update {UpdateId}.", update.UpdateId);
        }
    }
}




