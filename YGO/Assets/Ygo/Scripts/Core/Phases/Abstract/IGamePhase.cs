using System;
using Ygo.Core.Abstract;
using Ygo.Core.Board.Abstract;
using Ygo.Core.Enums;
using Ygo.Core.Response;

namespace Ygo.Core.Phases.Abstract
{
    public interface IGamePhase : IGamePhaseAction, IGamePhaseCommand
    {
        GamePhase Phase { get; }
        PhaseStep CurrentStep { get; }
        void Init();
    }
}