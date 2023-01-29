using System;

namespace Publisher
{
    public static class Publisher
    {
        private static Launcher _launcher;

        static Publisher()
        {
            _launcher = new Launcher();
        }

        public static void Publish<T>(this IPublisher<T> source, T message)
        {
            _launcher.Publish(source, message);
        }

        public static void Subscribe<T>(this IListener<T> source, Action<Payload<T>> subscription)
        {
            _launcher.Subscribe(subscription);
        }

        public static void Unsubscribe<T>(this IListener<T> source, Action<Payload<T>> subscription)
        {
            _launcher.Unsubscribe(subscription);
        }
    }
}