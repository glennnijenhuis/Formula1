using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {
        private List<IParticipant> participants;
        private Queue<Track> tracks;

        public Queue<Track> Tracks { get => tracks; set => tracks = value; }
        public List<IParticipant> Participants { get => participants; set => participants = value; }

        public Competition()
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();
        }

        public Track NextTrack()
        {
            if(Tracks.Count != 0)
            {
                return Tracks.Dequeue();
            } else
            {
                return null;
            }
        }
    }
}
