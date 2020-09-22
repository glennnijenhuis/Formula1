using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        private string name;
        private int points;
        private IEquipment iEquipment;
        private TeamColors teamColor;
        
        public string Name { get => name; set => name = value; }
        public int Points { get =>points; set => points = value; }
        public IEquipment IEquipment { get => iEquipment; set => iEquipment = value; }
        public TeamColors TeamColor { get => teamColor; set => teamColor = value; }

        public Driver(string name, int points, IEquipment iEquipment, TeamColors teamColor)
        {
            Name = name;
            Points = points;
            IEquipment = iEquipment;
            TeamColor = teamColor;
        }
    }
}
