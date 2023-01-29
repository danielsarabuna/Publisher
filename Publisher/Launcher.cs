using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publisher
{
    public class Launcher : IDisposable
    {
        private readonly Dictionary<Type, List<Delegate>> subscribers;

        public Launcher()
        {
            subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public void Dispose()
        {
            subscribers.Clear();
        }

        internal void Publish<T>(object source, T message)
        {
            if (source == null || message == null) return;
            if (!subscribers.ContainsKey(typeof(T))) return;

            var delegates = subscribers[typeof(T)];
            if (delegates == null || delegates.Count == 0) return;
            var payload = new Payload<T>(message, source);
            foreach (var handler in delegates.Select
                         (item => item as Action<Payload<T>>))
            {
                handler?.Invoke(payload);
            }
        }

        internal void Subscribe<T>(Action<Payload<T>> subscription)
        {
            var delegates = subscribers.ContainsKey(typeof(T)) ? subscribers[typeof(T)] : new List<Delegate>();
            if (!delegates.Contains(subscription)) delegates.Add(subscription);

            subscribers[typeof(T)] = delegates;
        }

        internal void Unsubscribe<T>(Action<Payload<T>> subscription)
        {
            if (!subscribers.ContainsKey(typeof(T))) return;
            var delegates = subscribers[typeof(T)];
            if (delegates.Contains(subscription)) delegates.Remove(subscription);
            if (delegates.Count == 0) subscribers.Remove(typeof(T));
        }
    }
}