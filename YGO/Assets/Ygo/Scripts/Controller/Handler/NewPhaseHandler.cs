using Cysharp.Threading.Tasks;
using Ygo.Controller.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Controller.Handler
{
    public class NewPhaseHandler : IHandler<INewPhaseMessage>
    {
        private readonly AnnouncementController _announcementController;
        
        public NewPhaseHandler(AnnouncementController announcementController)
        {
            _announcementController = announcementController;
        }
        
        public async UniTask HandleMessage(INewPhaseMessage message)
        {
            await _announcementController.DisplayAnnouncement($"{message.GamePhase}");
        }

        public async UniTask HandleMessage(IDuelMessage message)
        {
            await HandleMessage((INewPhaseMessage) message);
        }
    }
}