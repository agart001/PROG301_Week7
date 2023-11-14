using PROG301_Week7.Models;
using PROG301_Week7.Models.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.ViewModels.Serializable
{
    public class SerializableAerialVehicleViewModel : AerialVehicleViewModel
    {

        public new SerializableAerialVehicle aerialvehicle;

        public SerializableAerialVehicleViewModel(AerialVehicle av) : base(av)
        {
            var tmp = av as SerializableAerialVehicle;
            if (tmp != null) { aerialvehicle = tmp; }

            if(aerialvehicle == null)
            {
                throw new Exception("AerialVehicle NULL");
            }
        }

        public new Engine Engine
        {
            get
            {
                if (aerialvehicle.Engine == null) { throw new Exception("Null Engine"); }
                return aerialvehicle.Engine;
            }
            set { aerialvehicle.Engine = value; }
        }
        public new bool IsFlying { get { return aerialvehicle.IsFlying; } set { aerialvehicle.IsFlying = value; } }
        public new int MaxAltitude { get { return aerialvehicle.MaxAltitude; } set { aerialvehicle.MaxAltitude = value; } }
        public new int CurrentAltitude { get { return aerialvehicle.CurrentAltitude; } set { aerialvehicle.CurrentAltitude = value; } }
        public new int DefaultAltitudeChange { get { return aerialvehicle.DefaultAltitudeChange; } set { aerialvehicle.DefaultAltitudeChange = value; } }
        public new string TypeString { get { return aerialvehicle.TypeString; } set { aerialvehicle.TypeString = value; } }
    }
}
