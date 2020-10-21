using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceFinishedArgs : EventArgs
    {
        public Queue<IParticipant> Ranking;
        public Track Track;
    }
}
