using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PROG301_Week7.Models
{
    public class Airplane : AerialVehicle
    {
        public Airplane()
        {
            MaxAltitude = 41000;
        }

        public Airplane(int max)
        {
            MaxAltitude = max;
        }

    }
}
