using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PROG301_Week7.Models;

namespace PROG301_Week7.ViewModels
{
    public class AerialVehicleViewModel : BaseViewModel
    {
        public AerialVehicleViewModel(AerialVehicle av)
        {
            aerialvehicle = av;
        }

        public AerialVehicle aerialvehicle;

        public Engine Engine 
        { 
            get 
            { 
                if(aerialvehicle.Engine == null) { throw new Exception("Null Engine"); }
                return aerialvehicle.Engine; 
            } 
            set { aerialvehicle.Engine = value; } 
        }
        public bool IsFlying { get { return aerialvehicle.IsFlying; } set { aerialvehicle.IsFlying = value; } }
        public int MaxAltitude { get { return aerialvehicle.MaxAltitude; } set { aerialvehicle.MaxAltitude = value; } }
        public int CurrentAltitude { get { return aerialvehicle.CurrentAltitude; } set { aerialvehicle.CurrentAltitude = value; } }
        public int DefaultAltitudeChange { get { return aerialvehicle.DefaultAltitudeChange; } set { aerialvehicle.DefaultAltitudeChange = value; } }
        public string TypeString { get { return aerialvehicle.TypeString; } set { aerialvehicle.TypeString = value; } }
    }
}
