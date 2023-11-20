using PROG301_Week7.Interfaces;
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
    public class SerializableAerialVehicle : ISerializable, IFlyable, IEngine
    {
        // Private backing fields
        private Engine? engine;
        private bool isFlying;
        private int maxAltitude;
        private int currentAltitude;
        private int defaultAltitudeChange;
        private string typeString = string.Empty;

        //protected 
        protected Guid ID;

        // Public properties with getters and setters using the backing fields
        public Engine? Engine { get => engine; set => engine = value; }
        public bool IsFlying { get => isFlying; set => isFlying = value; }
        public int MaxAltitude { get => maxAltitude; set => maxAltitude = value; }
        public int CurrentAltitude { get => currentAltitude; set => currentAltitude = value; }
        public int DefaultAltitudeChange { get => defaultAltitudeChange; set => defaultAltitudeChange = value; }
        public string TypeString { get => typeString; set => typeString = value; }

        public SerializableAerialVehicle() { }


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

        public void FlyUp()
        {
            throw new NotImplementedException();
        }

        public void FlyUp(int HowManyFeet)
        {
            throw new NotImplementedException();
        }

        public void FlyDown()
        {
            throw new NotImplementedException();
        }

        public void FlyDown(int HowManyFeet)
        {
            throw new NotImplementedException();
        }

        public void StartEngine()
        {
            throw new NotImplementedException();
        }

        public void StopEngine()
        {
            throw new NotImplementedException();
        }

        public string getEngineStartedString()
        {
            throw new NotImplementedException();
        }
    }
}
