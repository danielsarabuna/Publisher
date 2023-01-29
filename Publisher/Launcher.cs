using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher
{
    public class Launcher : IDisposable
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers;

        public Launcher()
        {
            _subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public void Dispose()
        {
            _subscribers.Clear();
        }

        internal void Publish<T>(object source, T message)
        {
            if (source == null) return;
            if (!_subscribers.ContainsKey(typeof(T))) return;

            var delegates = _subscribers[typeof(T)];
            if (delegates == null || delegates.Count == 0) return;
            var payload = new Payload<T>(message, source);
            foreach (var handler in delegates.Select
                         (item => item as Action<Payload<T>>))
            {
                Task.Factory.StartNew(() => handler?.Invoke(payload));
            }
        }

        internal void Subscribe<T>(Action<Payload<T>> subscription)
        {
            var delegates = _subscribers.ContainsKey(typeof(T)) ? _subscribers[typeof(T)] : new List<Delegate>();
            if (!delegates.Contains(subscription)) delegates.Add(subscription);

            _subscribers[typeof(T)] = delegates;
        }

        internal void Unsubscribe<T>(Action<Payload<T>> subscription)
        {
            if (!_subscribers.ContainsKey(typeof(T))) return;
            var delegates = _subscribers[typeof(T)];
            if (delegates.Contains(subscription)) delegates.Remove(subscription);
            if (delegates.Count == 0) _subscribers.Remove(typeof(T));
        }
    }
}