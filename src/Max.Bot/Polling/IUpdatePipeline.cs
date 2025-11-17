// рџ“Ѓ [IUpdatePipeline] - РљРѕРЅС‚СЂР°РєС‚ РѕР±СЂР°Р±РѕС‚РєРё webhook Рё polling РѕР±РЅРѕРІР»РµРЅРёР№
// рџЋЇ Core function: РЈРЅРёС„РёС†РёСЂСѓРµС‚ API РѕР±СЂР°Р±РѕС‚РєРё РѕР±РЅРѕРІР»РµРЅРёР№ РґР»СЏ РєРѕРЅС‚СЂРѕР»Р»РµСЂР° Рё poller
// рџ”— Key dependencies: System.Threading.Tasks, Max.Bot.Types
// рџ’Ў Usage: Р РµР°Р»РёР·СѓРµС‚СЃСЏ MaxClient РґР»СЏ РїСЂРѕРєСЃРёСЂРѕРІР°РЅРёСЏ РѕР±РЅРѕРІР»РµРЅРёР№ РІ IUpdateHandler

using System;
using System.Threading;
using System.Threading.Tasks;
using Max.Bot.Types;

namespace Max.Bot.Polling;

/// <summary>
/// Defines operations required to dispatch updates to application handlers.
/// </summary>
public interface IUpdatePipeline
{
    /// <summary>
    /// Processes a webhook update payload.
    /// </summary>
    Task ProcessWebhookAsync(Update update, IUpdateHandler handler, IServiceProvider? services = null, CancellationToken cancellationToken = default);
}




