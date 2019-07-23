
using System;

using UnityEngine;


namespace FreakshowStudio.SerializableTimeStructures.Runtime
{
    [Serializable]
    public class SerializableTimeSpan
    {
        [SerializeField]
        private long _ticks;

        public SerializableTimeSpan()
        {
            _ticks = 0;
        }

        public SerializableTimeSpan(long ticks)
        {
            _ticks = ticks;
        }

        public SerializableTimeSpan(TimeSpan span)
        {
            _ticks = span.Ticks;
        }

        public static implicit operator TimeSpan(SerializableTimeSpan sts) =>
            new TimeSpan(sts._ticks);

        public static implicit operator SerializableTimeSpan(TimeSpan ts) =>
            new SerializableTimeSpan(ts.Ticks);
    }
}
