using System;
using System.Collections.Generic;

namespace Model
{
    public class ParticipantsTimeBroken : IRaceGegevensView
    {
        public IParticipant Participant { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public Track Track { get; set; }
        public override string ToString()
        {
            return $"{Participant.Name} : {TimeSpan} on track {Track.Name}";
        }

        public void Add(List<IRaceGegevensView> list)
        {
            foreach (IRaceGegevensView var in list)
            {
                ParticipantsTimeBroken gegevens = (ParticipantsTimeBroken)var;
                if (gegevens.Participant == this.Participant && gegevens.Track == this.Track)
                {
                    gegevens.TimeSpan += this.TimeSpan;
                    return;
                }
            }
            list.Add(this);
        }
        public string GetBestParticipant(List<IRaceGegevensView> list)
        {
            ParticipantsTimeBroken besteDeelnemer = new ParticipantsTimeBroken();
            foreach (IRaceGegevensView var in list)
            {
                ParticipantsTimeBroken gegevens = (ParticipantsTimeBroken)var;
                if (gegevens.TimeSpan < besteDeelnemer.TimeSpan)
                {
                    besteDeelnemer = gegevens;
                }
            }

            return $"De driver met de minste tijd kapot is: {besteDeelnemer.Participant.Name}";
        }
    }
}