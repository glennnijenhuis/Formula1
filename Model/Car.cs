using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Car : IEquipment
    {
        private int quality;
        private int performance;
        private int speed;
        private bool isBroken;

        public int Quality { get => quality; set => quality = value; }
        public int Performance { get => performance; set => performance = value; }
        public int Speed { get => speed; set => speed = value; }
        public bool IsBroken { get => isBroken; set => isBroken = value; }

        public Car(int quality, int performance, int speed, bool isBroken)
        {
            Quality = quality;
            Performance = performance;
            Speed = speed;
            IsBroken = isBroken;
        }


    }
}
