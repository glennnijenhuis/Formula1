using System;
using System.Collections.Generic;
using System.Text;


namespace Model
{
    public class ParticipantSectionTimes : IRaceGegevensView
    {
        public TimeSpan SectionTime { get; set; }
        public IParticipant Participant { get; set; }
        public Section Section { get; set; }

        public override string ToString()
        {
            return $"{Participant.Name} rijd de section : {Section.SectionType} in {SectionTime / 10000} milliseconden";
        }

        public void Add(List<IRaceGegevensView> list)
        {
            foreach (IRaceGegevensView var in list)
            {
                ParticipantSectionTimes gegevens = (ParticipantSectionTimes)var;
                if (gegevens.Participant == this.Participant && gegevens.Section == this.Section)
                {
                    gegevens.SectionTime = this.SectionTime;
                    return;
                }
            }
            list.Add(this);
        }
        public string GetBestParticipant(List<IRaceGegevensView> list)
        {
            ParticipantSectionTimes besteDeelnemer = new ParticipantSectionTimes();
            foreach (IRaceGegevensView var in list)
            {
                ParticipantSectionTimes gegevens = (ParticipantSectionTimes)var;
                if (gegevens.SectionTime < besteDeelnemer.SectionTime)
                {
                    besteDeelnemer = gegevens;
                }
            }

            return $"De driver met de laagste rondetijden is : {besteDeelnemer.Participant.Name}";
        }
    }
}

