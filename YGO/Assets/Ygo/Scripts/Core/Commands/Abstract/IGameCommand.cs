using System;

namespace Ygo.Core.Commands.Abstract
{
    public interface IGameCommand
    {
        Guid RequesterId { get; }
    }
}