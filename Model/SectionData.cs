using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SectionData
    {
        private IParticipant left;
        private int distanceLeft;
        private IParticipant right;
        private int distanceRight;

        public IParticipant Left { get => left; set => left = value; }
        public int DistanceLeft { get => distanceLeft; set => distanceLeft = value; }
        public IParticipant Right { get => right; set => right = value; }
        public int DistanceRight { get => distanceRight; set => distanceRight = value; }
    }
}
