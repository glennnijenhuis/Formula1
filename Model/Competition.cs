using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Competition
    {
        private List<IParticipant> participants;
        private Queue<Track> tracks;

        public Queue<Track> Tracks { get => tracks; set => tracks = value; }
        public List<IParticipant> Participants { get => participants; set => participants = value; }

        public RaceInfo<ParticipantPoints> ParticipantsPoints;
        public RaceInfo<ParticipantsQuality> ParticipantsQuality;
        public RaceInfo<ParticipantSectionTimes> ParticipantsSectionTimes;
        public RaceInfo<ParticipantsTimeBroken> ParticipantsTimeBroken { get; set; }
        public Competition()
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();
            
            ParticipantsPoints = new RaceInfo<ParticipantPoints>();
            ParticipantsQuality = new RaceInfo<ParticipantsQuality>();
            ParticipantsTimeBroken = new RaceInfo<ParticipantsTimeBroken>();
            ParticipantsSectionTimes = new RaceInfo<ParticipantSectionTimes>();
            AddStartingPoints();
        }

        public void AddStartingPoints()
        {
            foreach (IParticipant participant in Participants)
            {
                ParticipantsPoints.Add(new ParticipantPoints() { Points = 0, Participant = participant, Track = Tracks.First() });
            }
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
     
        public void AwardPoints(Queue<IParticipant> ranking, Track track)
        {
            int points = 25;
            while (ranking.Count > 0)
            {
                IParticipant participant = ranking.Dequeue();
                
                ParticipantsPoints.Add(new ParticipantPoints() { Points = points, Participant = participant, Track = track });
                participant.Points += points;
                points = points - 5;
                
            }
        }

        public void OnRaceFinished(object sender, RaceFinishedArgs args)
        {
            AwardPoints(args.Ranking, args.Track);
           

        }


    }
}
