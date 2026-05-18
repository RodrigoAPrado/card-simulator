using Cysharp.Threading.Tasks;
using Ygo.Controller.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Controller.Handler
{
    public class SelectChainHandler : IHandler<ISelectChainMessage>
    {
        public async UniTask HandleMessage(ISelectChainMessage message)
        {
            //message.Effects[0].
        }

        public async UniTask HandleMessage(IDuelMessage message)
        {
            await HandleMessage((ISelectChainMessage) message);
        }
    }
}