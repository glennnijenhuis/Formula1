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

        public RaceInfo<ParticipantPoints> ParticipantsPoints;
        public RaceInfo<ParticipantTotalTime> ParticipantsTime;

        public Competition()
        {
            Tracks = new Queue<Track>();
            Participants = new List<IParticipant>();

            ParticipantsPoints = new RaceInfo<ParticipantPoints>();
            ParticipantsTime = new RaceInfo<ParticipantTotalTime>();

             += OnRaceFinished;
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
            int points = 10;
            foreach (IParticipant participant in ranking)
            {
                ParticipantsPoints.Add(new ParticipantPoints() { Points = points, Name = participant.Name, Track = track });
                points += points / 2;
            }
        }

        public void OnRaceFinished(object sender, RaceFinishedArgs args)
        {
            AwardPoints(args.Ranking, args.Track);
            Console.WriteLine($"Track: {args.Track.Name}, uitslag:");
            while (args.Ranking.Count != 0)
            {
                Console.WriteLine($"Plek {args.Ranking.Count} is voor {args.Ranking.Dequeue().Name}");
            }
            Console.WriteLine("");
            Console.WriteLine(ParticipantsPoints.Print());
            //Console.WriteLine(KwaliteitGegevens.Print());
            Console.WriteLine("Wilt u door met de volgende race? (y/n)");
        }


    }
}
