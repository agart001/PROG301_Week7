using PROG301_Week7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7.Interfaces
{
    public interface IAirport
    {
        public List<AerialVehicle> Vehicles { get; set; }
        public int MaxVehicles { get; set; }
        public string AirportCode { get; set; }

        private static int defaultMaxVehicles = 5;
    }
}
