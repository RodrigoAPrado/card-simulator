using System;

namespace Ygo.Core.Response.Context.Abstract
{
    public interface IInteractionContext
    {
        Guid PlayerId { get; }
    }
}