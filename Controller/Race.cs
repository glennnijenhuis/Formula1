using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class Race
    {
        private Track track;
        private List<IParticipant> participants;
        private DateTime startTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;


        public Track Track { get => track; set => track = value; }
        public List<IParticipant> Participants { get => participants; set => participants = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            StartTime = DateTime.Now;
        }


        public SectionData GetSectionData(Section section)
        {
            SectionData value;
            bool hasValue = _positions.TryGetValue(section, out value);

            if (hasValue)
            {
                return value;
            } else
            {
                _positions.Add(section, new SectionData());
                bool hasValue2 = _positions.TryGetValue(section, out value);
                return value;
            }
        }

        public void RandomizeEquipment()
        {
            foreach(IParticipant participant in Participants)
            {
                participant.IEquipment.Quality = _random.Next();
                participant.IEquipment.Performance = _random.Next();
            }
        }
    }
}
