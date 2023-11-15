using PROG301_Week7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.Models.Serializable
{
    public class SerializableAirport : IAirport, ISerializable
    {
        //private vars
        protected List<AerialVehicle> vehicles;
        protected int maxvehicles;
        protected string airportcode;

        //public vars
        public List<AerialVehicle> Vehicles { get { return vehicles; } set { vehicles = value; } }
        public int MaxVehicles { get { return maxvehicles; } set { maxvehicles = value; } }
        public string AirportCode { get { return airportcode; } set { airportcode = value; } }

        public Guid ID { get; set; }

        public static int defaultMaxVehicles = 5;

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
