using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Driver : IParticipant
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEquipment IEquipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TeamColors TeamColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Driver(string name, int points, IEquipment iEquipment, TeamColors teamColor)
        {
            Name = name;
            Points = points;
            IEquipment = iEquipment;
            TeamColor = teamColor;
        }
    }
}
