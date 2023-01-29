using System;

namespace Publisher
{
    public struct Payload<T> where T : struct
    {
        public object Who { get; }
        public T What { get; }
        public DateTime When { get; }

        public Payload(T payload, object source)
        {
            Who = source;
            What = payload;
            When = DateTime.UtcNow;
        }
    }
}