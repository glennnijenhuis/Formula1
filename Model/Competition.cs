using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Competition
    {
        private List<IParticipant> participants;
        private Queue<Track> tracks;

        public Queue<Track> Tracks { get => tracks; set => tracks = value; }
        internal List<IParticipant> Participants { get => participants; set => participants = value; }
    }
}
