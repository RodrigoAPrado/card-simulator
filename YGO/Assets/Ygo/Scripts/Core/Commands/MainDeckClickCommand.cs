using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Commands
{
    public class MainDeckClickCommand : IGameCommand
    {
        public bool PoV { get; }
        
        public MainDeckClickCommand(bool poV)
        {
            PoV = poV;
        }
    }
}