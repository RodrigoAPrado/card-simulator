using System;
using System.Collections;
using System.Collections.Generic;

namespace Ygo.Controller.Handler.Base
{
    public class HandlerRegistry
    {
        private readonly Dictionary<Type, IHandler> _handlers;

        public HandlerRegistry(Dictionary<Type, IHandler> handlers)
        {
            _handlers = handlers;
        }

        public IHandler GetHandler<T>()
        {
            _handlers.TryGetValue(typeof(T), out IHandler handler);
            return handler;
        }
    }
}