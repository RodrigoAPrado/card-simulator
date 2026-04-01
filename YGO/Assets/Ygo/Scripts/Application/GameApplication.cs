using Ygo.Scripts.Core;

namespace Ygo.Scripts.Application
{
    public class GameApplication
    {
        private ICardRepository CardRepository { get; set; }
        
        public GameApplication(ICardRepository cardRepository)
        {
            CardRepository = cardRepository;
        }
    }
}