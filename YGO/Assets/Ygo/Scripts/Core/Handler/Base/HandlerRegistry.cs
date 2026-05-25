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
                {typeof(IDrawMessage), new DrawHandler()},
                {typeof(INewTurnMessage), new NewTurnHandler()},
                {typeof(INewPhaseMessage), new NewPhaseHandler()},
                {typeof(ISelectChainMessage), new SelectChainHandler()},
                {typeof(ISelectPlaceMessage), new SelectPlaceHandler()},
                {typeof(IMoveMessage), new MoveHandler()}
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
                case IDrawMessage:
                    _handlers.TryGetValue(typeof(IDrawMessage), out handler);
                    break;
                case INewTurnMessage:
                    _handlers.TryGetValue(typeof(INewTurnMessage), out handler);
                    break;
                case INewPhaseMessage:
                    _handlers.TryGetValue(typeof(INewPhaseMessage), out handler);
                    break;
                case ISelectChainMessage:
                    _handlers.TryGetValue(typeof(ISelectChainMessage), out handler);
                    break;
                case ISelectPlaceMessage:
                    _handlers.TryGetValue(typeof(ISelectPlaceMessage), out handler);
                    break;
                case IMoveMessage:
                    _handlers.TryGetValue(typeof(IMoveMessage), out handler);
                    break;
            }

            return handler;
        }
    }
}