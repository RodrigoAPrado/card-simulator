using System;
using System.Collections.Generic;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message;
using YgoSoul.RapTech.Lib.YgoEdo.Abstractions.Message.Base;

namespace Ygo.Scripts.Core.Handler.Base
{
    public class HandlerRegistry
    {
        private readonly Dictionary<Type, IHandler> _handlers;

        public static HandlerRegistry Create()
        {
            var handlers = new Dictionary<Type, IHandler>
            {
            };

            return new HandlerRegistry(handlers);
        }
        
        private HandlerRegistry(Dictionary<Type, IHandler> handlers)
        {
            _handlers = handlers;
        }

        public IHandler GetHandler(IDuelMessage message)
        {
            IHandler handler = null;
            
            switch (message)
            {
                case IDrawMessage drawMessage:
                    _handlers.TryGetValue(drawMessage.GetType(), out handler);
                    break;
            }

            return handler;
        }
    }
}


        
        
/**
 *
 *
 *
 * case IDrawMessage drawMessage:
            await _handlerRegistry.GetHandler<IDrawMessage>().HandleMessage(drawMessage);
            break;
        case INewTurnMessage newTurnMessage:
            await _handlerRegistry.GetHandler<INewTurnMessage>().HandleMessage(newTurnMessage);
            break;
        case INewPhaseMessage newPhaseMessage:
            await _handlerRegistry.GetHandler<INewPhaseMessage>().HandleMessage(newPhaseMessage);
            break;
 **/