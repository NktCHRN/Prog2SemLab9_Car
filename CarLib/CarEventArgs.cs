using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLib
{
    public class CarEventArgs : EventArgs
    {
        public int PreviousMileage { get; private set; }                        // mileage was (km)
        public double Distance { get; private set; }                            // distance (km)
        public int MaxMileage { get; private set; }                             // maximum mileage (km)
        public CarEventArgs(int previousMileage, double distance, int maxMileage)
        {
            if (previousMileage >= 0)
                PreviousMileage = previousMileage;
            else
                throw new ArgumentOutOfRangeException(nameof(previousMileage), "Previous mileage cannot be negative");
            if (distance >= 0)
                Distance = distance;
            else
                throw new ArgumentOutOfRangeException(nameof(distance), "Distance cannot be negative");
            if (maxMileage >= 0)
                MaxMileage = maxMileage;
            else
                throw new ArgumentOutOfRangeException(nameof(maxMileage), "Maximum mileage cannot be negative");
        }
    }
}
