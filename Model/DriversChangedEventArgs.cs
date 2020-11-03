using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    
        public delegate void DriversChanged(DriversChangedEventArgs driversChangedEventArgs);
        

        public class DriversChangedEventArgs : EventArgs
        {
            public Track Track
            {
                get;
                set;
            }

            public DriversChangedEventArgs(Track Track)
            {
                this.Track = Track;
            }



        }
    
}
