using System;
using System.Collections.Generic;
using Ygo.Core.Commands.Abstract;

namespace Ygo.Core
{
    public class GameCommandBus
    {
        private readonly Dictionary<Type, Action<IGameCommand>> _handlers = new();

        public void RegisterHandler<T>(Action<T> handler) where T : IGameCommand
        {
            var type = typeof(T);
            _handlers[type] = (gameCommand) => handler((T)gameCommand);
        }

        public void Send(IGameCommand command)
        {
            var type = command.GetType();
            if (!_handlers.TryGetValue(type, out var handler))
            {
                throw new InvalidOperationException($"No handler registered for {type.Name}");
            }
            handler(command);
        }
    }
}