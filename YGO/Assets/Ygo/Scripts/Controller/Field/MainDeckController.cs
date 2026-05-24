using Cysharp.Threading.Tasks;
using Ygo.Scripts.Core.Event;
using Ygo.Scripts.Core.Event.Base;

namespace Ygo.Controller.Field
{
    public class MainDeckController : DeckController
    {
        public override void Init(int deckSize, EventQueue eventQueue)
        {
            base.Init(deckSize, eventQueue);
            eventQueue.Subscribe<DrawDeckEvent>(OnDrawEvent);
        }

        private async UniTask OnDrawEvent(DrawDeckEvent e)
        {
            //TODO: Animate
            SetDeckCount(e.AmountAfter);
        }
    }
}