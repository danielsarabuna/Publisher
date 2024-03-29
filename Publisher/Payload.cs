﻿using System;

namespace Publisher
{
    public class Payload<T>
    {
        public object Who { get; private set; }
        public T What { get; private set; }
        public DateTime When { get; private set; }

        public Payload(T payload, object source)
        {
            Who = source;
            What = payload;
            When = DateTime.UtcNow;
        }
    }
}