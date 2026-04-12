using UnityEngine.Assertions.Must;

namespace Ygo.Core.Enums
{
    public enum PhaseStep
    {
        None = 0,
        WaitingDraw = 1,
        Open = 2,
        Battle = 3,
        ProceedToNextPhase = 99,
    }
}