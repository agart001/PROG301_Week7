using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PROG301_Week7.Models.Serializable
{
    public class SerializableAerialVehicle : AerialVehicle, ISerializable
    {
        protected SerializableAerialVehicle(SerializationInfo info, StreamingContext context)
        {
            Engine = info.GetValue("Engine", typeof(Engine)) as Engine ?? new Engine();

            IsFlying = info.GetBoolean("IsFlying");

            MaxAltitude = info.GetInt32("MaxAltitude");
            CurrentAltitude = info.GetInt32("CurrentAltitude");
            DefaultAltitudeChange = info.GetInt32("DefaultAltitudeChange");

            TypeString = info.GetString("TypeString") ?? string.Empty;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Engine", Engine);

            info.AddValue("IsFlying", IsFlying);

            info.AddValue("MaxAltitude", MaxAltitude);
            info.AddValue("CurrentAltitude", CurrentAltitude);
            info.AddValue("DefaultAltitudeChange", DefaultAltitudeChange);

            info.AddValue("TypeString", TypeString);
        }
    }
}
