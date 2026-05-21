using System.Collections.Generic;
using System.Windows.Input;
using Cysharp.Threading.Tasks;
using Ygo.Core.Duel;
using Ygo.Scripts.Core.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;
using ICommand = Ygo.Scripts.Core.Command.Base.ICommand;

namespace Ygo.Scripts.Core.Handler
{
    public class DrawHandler : IHandler<IDrawMessage>
    {
        public UniTask<IReadOnlyList<ICommand>> HandleMessage(IDrawMessage message, DuelState duelState)
        {
            List<ICommand> commands = new List<ICommand>();
            
            foreach (var card in message.Cards)
            {
                commands.Add(duelState.DrawCard(card.CardCode, message.Player));
            }

            return new UniTask<IReadOnlyList<ICommand>>(commands);
        }

        public async UniTask<IReadOnlyList<ICommand>> HandleMessage(IDuelMessage message, DuelState duelState)
        {
            return await HandleMessage((IDrawMessage)message, duelState);
        }
    }
}