// рџ“Ѓ [UpdateHandlerBase] - Р‘Р°Р·РѕРІС‹Р№ РєР»Р°СЃСЃ РѕР±СЂР°Р±РѕС‚С‡РёРєР° РѕР±РЅРѕРІР»РµРЅРёР№
// рџЋЇ Core function: РџСЂРµРґРѕСЃС‚Р°РІР»СЏРµС‚ no-op СЂРµР°Р»РёР·Р°С†РёРё IUpdateHandler
// рџ”— Key dependencies: System.Threading.Tasks
// рџ’Ў Usage: РќР°СЃР»РµРґСѓРµС‚СЃСЏ РїРѕР»СЊР·РѕРІР°С‚РµР»РµРј РґР»СЏ РїРµСЂРµРѕРїСЂРµРґРµР»РµРЅРёСЏ РЅСѓР¶РЅС‹С… РјРµС‚РѕРґРѕРІ

using System.Threading;
using System.Threading.Tasks;

namespace Max.Bot.Polling;

/// <summary>
/// Convenience base class mirroring Telegram.Bot's <c>IUpdateHandler</c> defaults.
/// </summary>
public abstract class UpdateHandlerBase : IUpdateHandler
{
    /// <inheritdoc />
    public virtual Task HandleUpdateAsync(UpdateContext context, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    /// <inheritdoc />
    public virtual Task HandleMessageAsync(UpdateContext context, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    /// <inheritdoc />
    public virtual Task HandleCallbackQueryAsync(UpdateContext context, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    /// <inheritdoc />
    public virtual Task HandleUnknownUpdateAsync(UpdateContext context, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}




