using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Ygo.Controller.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Controller.Handler
{
    public class DrawHandler : IHandler<IDrawMessage>
    {
        private readonly Dictionary<int, HandController> _handControllers;

        public DrawHandler(Dictionary<int, HandController> handControllers)
        {
            _handControllers = handControllers;
        }

        public async UniTask HandleMessage(IDrawMessage message)
        {
            var handController = _handControllers[message.Player];
            await handController.Draw(message.Cards.Select(x => x.CardCode).ToList());
        }

        public async UniTask HandleMessage(IDuelMessage message)
        {
            await HandleMessage((IDrawMessage) message);
        }
    }
}