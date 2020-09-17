using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment IEquipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }

    enum TeamColors
    {
        Red,
        Greeen,
        Yellow,
        Grey,
        Blue
    }
}
