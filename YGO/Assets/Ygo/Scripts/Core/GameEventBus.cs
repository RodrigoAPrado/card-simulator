using System;
using System.Collections.Generic;
using Ygo.Core.Events.Abstract;

namespace Ygo.Core
{
    public class GameEventBus
    {
        private readonly Dictionary<Type, List<Action<IGameEvent>>> _subscribers = new();

        public void Subscribe<T>(Action<T> callback) where T : IGameEvent
        {
            var type = typeof(T);
            
            if(!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Action<IGameEvent>>();
            
            _subscribers[type].Add(e => callback((T)e));
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IGameEvent
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var subscriber))
                return;

            subscriber.RemoveAll(x => x.Equals(callback));
        }

        public void Publish(IGameEvent gameEvent)
        {
            var type = gameEvent.GetType();

            if (!_subscribers.TryGetValue(type, out var subscriber))
                return;

            foreach (var callback in subscriber)
            {
                callback.Invoke(gameEvent);
            }
        }
    }
}