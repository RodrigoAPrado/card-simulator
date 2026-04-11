using Ygo.Core.Commands.Abstract;

namespace Ygo.Core.Interaction.Abstract
{
    public interface IInteractionState
    {
        void Handle(IGameCommand command);
    }
}