// рџ“Ѓ [DelegatingUpdateHandler] - РђРґР°РїС‚РµСЂ РѕР±СЂР°Р±РѕС‚С‡РёРєР° РѕР±РЅРѕРІР»РµРЅРёР№
// рџЋЇ Core function: РџРѕР·РІРѕР»СЏРµС‚ РЅР°СЃС‚СЂР°РёРІР°С‚СЊ РѕР±СЂР°Р±РѕС‚С‡РёРєРё С‡РµСЂРµР· РґРµР»РµРіР°С‚С‹
// рџ”— Key dependencies: System, System.Threading.Tasks, Max.Bot.Types.Enums
// рџ’Ў Usage: Р‘С‹СЃС‚СЂР°СЏ РёРЅС‚РµРіСЂР°С†РёСЏ Р»СЏРјР±РґР°-РѕР±СЂР°Р±РѕС‚С‡РёРєРѕРІ Р±РµР· РЅР°СЃР»РµРґРѕРІР°РЅРёСЏ

using System;
using System.Threading;
using System.Threading.Tasks;
using Max.Bot.Types.Enums;

namespace Max.Bot.Polling;

/// <summary>
/// Delegate-driven <see cref="IUpdateHandler"/> implementation for lightweight scenarios and tests.
/// </summary>
public sealed class DelegatingUpdateHandler : IUpdateHandler
{
    private readonly Func<UpdateContext, CancellationToken, Task>? _onUpdate;
    private readonly Func<UpdateContext, CancellationToken, Task>? _onMessage;
    private readonly Func<UpdateContext, CancellationToken, Task>? _onCallback;
    private readonly Func<UpdateContext, CancellationToken, Task>? _onUnknown;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegatingUpdateHandler"/> class.
    /// </summary>
    /// <param name="onUpdate">Delegate executed for every update prior to type dispatch.</param>
    /// <param name="onMessage">Delegate executed for <see cref="UpdateType.Message"/> events.</param>
    /// <param name="onCallback">Delegate executed for <see cref="UpdateType.CallbackQuery"/> events.</param>
    /// <param name="onUnknown">Delegate executed for unsupported update types.</param>
    public DelegatingUpdateHandler(
        Func<UpdateContext, CancellationToken, Task>? onUpdate = null,
        Func<UpdateContext, CancellationToken, Task>? onMessage = null,
        Func<UpdateContext, CancellationToken, Task>? onCallback = null,
        Func<UpdateContext, CancellationToken, Task>? onUnknown = null)
    {
        _onUpdate = onUpdate;
        _onMessage = onMessage;
        _onCallback = onCallback;
        _onUnknown = onUnknown;
    }

    /// <inheritdoc />
    public Task HandleUpdateAsync(UpdateContext context, CancellationToken cancellationToken)
    {
        return _onUpdate?.Invoke(context, cancellationToken) ?? Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task HandleMessageAsync(UpdateContext context, CancellationToken cancellationToken)
    {
        return _onMessage?.Invoke(context, cancellationToken) ?? Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task HandleCallbackQueryAsync(UpdateContext context, CancellationToken cancellationToken)
    {
        return _onCallback?.Invoke(context, cancellationToken) ?? Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task HandleUnknownUpdateAsync(UpdateContext context, CancellationToken cancellationToken)
    {
        return _onUnknown?.Invoke(context, cancellationToken) ?? Task.CompletedTask;
    }
}




