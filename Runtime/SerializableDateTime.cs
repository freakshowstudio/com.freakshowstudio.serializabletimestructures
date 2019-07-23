
using System;

using UnityEngine;


namespace FreakshowStudio.SerializableTimeStructures.Runtime
{
    [Serializable]
    public class SerializableDateTime
    {
        [SerializeField]
        private long _ticks;

        public SerializableDateTime()
        {
            _ticks = 0;
        }

        public SerializableDateTime(long ticks)
        {
            _ticks = ticks;
        }

        public SerializableDateTime(DateTime dateTime)
        {
            _ticks = dateTime.Ticks;
        }

        public static implicit operator DateTime(SerializableDateTime sdt) =>
            new DateTime(sdt._ticks);

        public static implicit operator SerializableDateTime(DateTime dt) =>
            new SerializableDateTime(dt.Ticks);
    }
}
