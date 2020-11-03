using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ParticipantsQuality : IRaceGegevensView
    {
        public IParticipant Participant { get; set; }
        public int QualityBeforeRace { get; set; }
        public int QualityAfterRace { get; set; }
        public Track Track { get; set; }
        public override string ToString()
        {
            return $"{Participant.Name} : kwaliteit voor race {QualityBeforeRace}, na race {QualityAfterRace} op track {Track.Name}";
        }

        public void Add(List<IRaceGegevensView> list)
        {
            foreach (IRaceGegevensView var in list)
            {
                ParticipantsQuality gegevens = (ParticipantsQuality)var;

                if (gegevens.Participant == this.Participant && gegevens.Track == this.Track)
                {
                    gegevens.QualityAfterRace = this.QualityAfterRace;
                    gegevens.QualityBeforeRace = this.QualityBeforeRace;
                    return;
                }
            }
            list.Add(this);
        }
        public string GetBestParticipant(List<IRaceGegevensView> list)
        {
            ParticipantsQuality besteDeelnemer = new ParticipantsQuality();
            foreach (IRaceGegevensView var in list)
            {
                ParticipantsQuality gegevens = (ParticipantsQuality)var;
                if (gegevens.QualityAfterRace > besteDeelnemer.QualityAfterRace)
                {
                    besteDeelnemer = gegevens;
                }
            }
            return $"De driver met de meeste kwaliteit na de race is : {besteDeelnemer.Participant.Name} op de track : {besteDeelnemer.Track.Name}";
        }
    }
}
