using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Controller
{
    public class Race
    {
        private Track track;
        private List<IParticipant> participants;
        private DateTime startTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        private Timer timer = new Timer(500);

        public Track Track { get => track; set => track = value; }
        public List<IParticipant> Participants { get => participants; set => participants = value; }
        public DateTime StartTime { get => startTime; set => startTime = value; }

        private int LapsLeft = 0;
        private int LapsRight = 0;

        public event DriversChanged DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            StartTime = DateTime.Now;
            
            _positions = new Dictionary<Section, SectionData>();
            AddParticipantsToTrack(track, participants);
            
            timer.Elapsed += OnTimedEvent;
            
            Start();
        }

        public void AddParticipantsToTrack(Track track, List<IParticipant> participants)
        {
            int count = 0;

            var TrackSections = track.Sections;
                foreach (Section s in TrackSections)
                {
                    if (s.SectionType == SectionTypes.StartGrid && count < participants.Count)
                    {
                        SectionData sectionData = GetSectionData(s);
                        sectionData.Left = participants[count];
                        sectionData.DistanceLeft = 0;
                        count++;
                        sectionData.Right = participants[count];
                        sectionData.DistanceRight = 0;
                        count++;
                    }

                }
            

         
        }

        

        public SectionData GetSectionData(Section section)
        {
            if(_positions.TryGetValue(section, out SectionData data))
            {
                return data;
            }
            else
            {
                _positions.Add(section, new SectionData());
                return _positions[section];
            }
      
        }

        public void RandomizeEquipment()
        {
            foreach(IParticipant participant in Participants)
            {
                participant.IEquipment.Quality = _random.Next(6,10);
                participant.IEquipment.Performance = _random.Next(6,10);
            }
        }

        public void OnTimedEvent(object sender, EventArgs e)
        {
            int speedParticipant = 0;
            RandomizeEquipment();
            for (int i = 0; i < Track.Sections.Count; i++)
            {
                
                KeyValuePair<Section, SectionData> entry = _positions.ElementAt(i);
                SectionData sd = GetSectionData(entry.Key);



                if (sd.Left != null)
                {
                    speedParticipant = sd.Left.IEquipment.Speed * sd.Left.IEquipment.Performance;

                    
                        sd.DistanceLeft += speedParticipant;
                        if (sd.DistanceLeft > 100)
                        {
                            sd.DistanceLeft = 0;
                            sd.Left = null;


                            if (entry.Key.SectionType != SectionTypes.Finish)
                            {
                                KeyValuePair<Section, SectionData> entryNext = _positions.ElementAt(i + 1);
                                sd = GetSectionData(entryNext.Key);
                                sd.Left = Participants[0];
                                sd.DistanceLeft = 0;


                                DriversChanged(new DriversChangedEventArgs(Track));
                            }

                            else
                            {
                                KeyValuePair<Section, SectionData> entryNext = _positions.ElementAt(0);
                                sd = GetSectionData(entryNext.Key);
                                sd.Left = Participants[0];
                                sd.DistanceLeft = 0;

                                DriversChanged(new DriversChangedEventArgs(Track));
                            }
                            if (entry.Key.SectionType.Equals(SectionTypes.Finish))
                            {
                                LapsLeft++;
                                if (LapsLeft == 3)
                                {
                                    sd.Left = null;
                                }
                            }

                        }
                    
                }
                if (sd.Right != null)
                {
                    speedParticipant = sd.Right.IEquipment.Speed * sd.Right.IEquipment.Performance;

                    
                        sd.DistanceRight += speedParticipant;
                        if (sd.DistanceRight > 100)
                        {
                            sd.DistanceRight = 0;
                            sd.Right = null;


                            if (entry.Key.SectionType != SectionTypes.Finish)
                            {
                                KeyValuePair<Section, SectionData> entryNext = _positions.ElementAt(i + 1);
                                sd = GetSectionData(entryNext.Key);

                                sd.Right = Participants[1];
                                sd.DistanceRight = 0;
                                DriversChanged(new DriversChangedEventArgs(Track));
                            }
                            else
                            {
                                KeyValuePair<Section, SectionData> entryNext = _positions.ElementAt(0);
                                sd = GetSectionData(entryNext.Key);

                                sd.Right = Participants[1];
                                sd.DistanceRight = 0;
                                DriversChanged(new DriversChangedEventArgs(Track));


                            }
                            if (entry.Key.SectionType.Equals(SectionTypes.Finish))
                            {
                                LapsRight++;
                                if (LapsRight == 3)
                                {
                                    sd.Right = null;
                                DriversChanged(new DriversChangedEventArgs(Track));
                            }
                            }

                        }
                    
                }
            }
        }

        public void Start()
        {
            timer.Start();
        }
    }
}
