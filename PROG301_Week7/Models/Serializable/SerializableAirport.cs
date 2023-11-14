using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.Models.Serializable
{
    public class SerializableAirport : Airport, ISerializable
    {
        public SerializableAirport() { }


        protected SerializableAirport(SerializationInfo info, StreamingContext context)
        {

            var tmpvl = info.GetValue("Vehicles", typeof(List<AerialVehicle>)) as List<AerialVehicle>;
            if (tmpvl != null) { Vehicles = tmpvl; }
            else { Vehicles =  new List<AerialVehicle>();}

            MaxVehicles = info.GetInt32("MaxVehicles");
            AirportCode = info.GetString("AirportCode") ?? string.Empty;

            var tmpid = (Guid)info.GetValue("ID", typeof(Guid));
            if(Equals(tmpid, null)) { throw new Exception("GUID ID NULL"); }
            else { ID = tmpid; }

        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Vehicles", Vehicles);
            info.AddValue("MaxVehicles", MaxVehicles);
            info.AddValue("AirportCode", AirportCode);
            info.AddValue("ID", ID);
        }
    }
}
