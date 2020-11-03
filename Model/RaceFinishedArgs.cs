using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public delegate void RaceFinished(object sender, RaceFinishedArgs raceFinishedArgs);

    public class RaceFinishedArgs : EventArgs
    {
        public Queue<IParticipant> Ranking { get; set; }
        public Track Track { get; set; }
    }
}
