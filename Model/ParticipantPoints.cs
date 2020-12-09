using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ParticipantPoints : IRaceGegevensView
    {
        public IParticipant Participant { get; set; }
        public int Points;
        public Track Track;
        public override string ToString()
        {
            return $"{Participant.Name} heeft {Points} behaald op Track: {Track.Name}";
        }
        public void Add(List<IRaceGegevensView> list)
        {
            foreach (IRaceGegevensView var in list)
            {
                ParticipantPoints gegevens = (ParticipantPoints)var;
                if (gegevens.Participant == this.Participant)
                {
                    Console.WriteLine(gegevens.Points + " " + this.Points);
                    gegevens.Points += this.Points;
                    Console.WriteLine();
                    return;
                }
            }
            list.Add(this);
        }

        public string GetBestParticipant(List<IRaceGegevensView> list)
        {
            if (list.Count == 0)
            {
                return "";
            }
            ParticipantPoints besteDeelnemer = new ParticipantPoints();
            foreach (IRaceGegevensView var in list)
            {
                ParticipantPoints gegevens = (ParticipantPoints)var;
                if (gegevens.Points > besteDeelnemer.Points)
                {
                    besteDeelnemer = gegevens;
                }
            }

            return $"De meeste punten zijn voor: {besteDeelnemer.Participant.Name} hij heeft {besteDeelnemer.Participant.Points} punten";
        }
    }
}
