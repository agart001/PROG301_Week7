using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.Models.Serializable
{
    public sealed class SerializableEngine : Engine, ISerializable
    {
        public SerializableEngine(SerializationInfo info, StreamingContext context)
        {
            IsStarted = info.GetBoolean("IsStarted");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsStarted", IsStarted);
        }
    }
}
