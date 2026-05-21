using System;
using System.Collections.Generic;

namespace Ygo.Scripts.Core.Event.Base
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Action<IEvent>>> _subscribers = new();

        public void Subscribe<T>(Action<T> callback) where T : IEvent
        {
            var type = typeof(T);
            
            if(!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Action<IEvent>>();
            
            _subscribers[type].Add(e => callback((T)e));
        }

        public void Unsubscribe<T>(Action<T> callback) where T : IEvent
        {
            var type = typeof(T);

            if (!_subscribers.TryGetValue(type, out var subscriber))
                return;

            subscriber.RemoveAll(x => x.Equals(callback));
        }

        public void PublishMany(IReadOnlyCollection<IEvent> events)
        {
            foreach (var e in events)
            {
                Publish(e);
            }
        }

        private void Publish(IEvent gameEvent)
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