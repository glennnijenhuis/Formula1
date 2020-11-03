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
            foreach (IParticipant participant in ranking)
            {
                ParticipantsPoints.Add(new ParticipantPoints() { Points = points, Participant = participant, Track = track });
                participant.Points += points;
                points = points - 5;
                
            }
        }

        public void OnRaceFinished(object sender, RaceFinishedArgs args)
        {
            AwardPoints(args.Ranking, args.Track);
            Console.WriteLine($"Track: {args.Track.Name}, uitslag:");
            int count = args.Ranking.Count;
            for (int i = 1; i <= count; i++)
            {
                Console.WriteLine($"Plek {i} is voor {args.Ranking.Dequeue().Name}");
            }

            Console.WriteLine("");
            Console.WriteLine(ParticipantsPoints.Print());
            Console.WriteLine(ParticipantsQuality.Print());
            Console.WriteLine("");
            foreach (IParticipant participant in Participants)
            {
                Console.WriteLine($"{participant.Name} heeft {participant.Points} punten");
            }
            Console.WriteLine("");
            Console.WriteLine("Next Race? (y/n)");

            string console = Console.ReadLine();
            while (console != "y")
            {
                console = Console.ReadLine();
            }

        }


    }
}
