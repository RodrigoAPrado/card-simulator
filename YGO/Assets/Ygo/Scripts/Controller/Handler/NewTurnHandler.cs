using Cysharp.Threading.Tasks;
using Ygo.Controller.Handler.Base;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Controller.Handler
{
    public class NewTurnHandler : IHandler<INewTurnMessage>
    {
        private readonly AnnouncementController _announcementController;
        
        public NewTurnHandler(AnnouncementController announcementController)
        {
            _announcementController = announcementController;
        }
        
        public async UniTask HandleMessage(INewTurnMessage message)
        {
            await _announcementController.DisplayAnnouncement("New Turn!");
        }

        public async UniTask HandleMessage(IDuelMessage message)
        {
            await HandleMessage((INewTurnMessage) message);
        }
    }
}