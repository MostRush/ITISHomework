using System;

namespace Indexators
{
    class Station
    {
        public DateTime TimeOfArrival_2 { get; set; }

        private DateTime? timeOfArrival;
        private DateTime? timeOfDeparture;

        public DateTime? TimeOfArrival
        {
            get
            {
                return timeOfArrival;
            }

            set
            {
                timeOfArrival = value;
            }
        }

        public DateTime? TimeOfDeparture
        {
            get
            {
                return timeOfDeparture;
            }

            set
            {
                timeOfDeparture = value < timeOfArrival ? timeOfDeparture : value;
            }
        }
    }
}
