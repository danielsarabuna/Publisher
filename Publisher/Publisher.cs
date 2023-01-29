using System;

namespace Publisher
{
    public static class Publisher
    {
        private static Launcher launcher;

        static Publisher()
        {
            launcher = new Launcher();
        }

        public static void Publish<T>(this IPublisher<T> source, T message)
        {
            launcher.Publish(source, message);
        }

        public static void Subscribe<T>(this IListener<T> source, Action<Payload<T>> subscription)
        {
            launcher.Subscribe(subscription);
        }

        public static void Unsubscribe<T>(this IListener<T> source, Action<Payload<T>> subscription)
        {
            launcher.Unsubscribe(subscription);
        }
    }
}