using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Ygo.Scripts.Core.Event.Base
{
    public class EventQueue
    {
        private readonly Dictionary<Type, List<Func<IEvent, UniTask>>> _subscribers = new();
        private readonly Queue<IEvent> _eventQueue = new();
        private readonly CancellationTokenSource _cts = new();
        private bool _isProcessing = false;
        
        public void Subscribe<T>(Func<T, UniTask> callback) where T : IEvent
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Func<IEvent, UniTask>>();
        
            _subscribers[type].Add(e => callback((T)e));
        }
        
        public void EnqueueEvent(IEvent e) 
        {
            lock (_eventQueue)
            {
                _eventQueue.Enqueue(e);
            }
            TryProcessNextEvent().Forget(); 
        }
        
        private async UniTaskVoid TryProcessNextEvent()
        {
            if (_isProcessing) return;
            _isProcessing = true;

            try
            {
                while (true)
                {
                    IEvent nextEvent = null;

                    lock (_eventQueue)
                    {
                        if (_eventQueue.Count > 0)
                        {
                            nextEvent = _eventQueue.Dequeue();
                        }
                    }

                    if (nextEvent == null) break;

                    await ProcessEvent(nextEvent);
                }
            }
            finally
            {
                _isProcessing = false;
            }
        }
        
        private async UniTask ProcessEvent(IEvent e)
        {
            var type = e.GetType();
            if (!_subscribers.TryGetValue(type, out var callbacks)) return;

            var tasks = new List<UniTask>();
            foreach (var callback in callbacks)
            {
                tasks.Add(callback(e));
            }

            await UniTask.WhenAll(tasks).AttachExternalCancellation(_cts.Token);
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}