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
        private int Laps = 3;
        public event DriversChanged DriversChanged;
        public event RaceFinished RaceFinishedEvent;
        private Queue<IParticipant> DriversFinished;

        public Race(Track track, List<IParticipant> participants)
        {
           
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            StartTime = DateTime.Now;
            DriversFinished = new Queue<IParticipant>();
            _positions = new Dictionary<Section, SectionData>();
            AddParticipantsToTrack(track, participants);
            
            timer.Elapsed += OnTimedEvent;
            RandomizeEquipment();
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
                participant.IEquipment.Quality = _random.Next(1,3);
                participant.IEquipment.Performance = _random.Next(6,10);
            }
        }

       public void IsBroken()
        {
            foreach(IParticipant participant in Participants)
            {
                int i = _random.Next(1, 100);
                int breakChance = participant.IEquipment.Quality;

                int total = i - breakChance;

                if (participant.IEquipment.IsBroken && i < 40)
                {
                    participant.IEquipment.IsBroken = false;
                    
                    
                    if (participant.IEquipment.Performance > 1)
                    {
                        participant.IEquipment.Performance--;
                    } else
                    {
                        if (participant.IEquipment.Speed > 1)
                        {
                            participant.IEquipment.Speed--;
                        }
                    }
                    if (participant.IEquipment.Quality > 1)
                    {
                        participant.IEquipment.Quality--;
                    }
                } else if (!participant.IEquipment.IsBroken && total > 95)
                {
                    participant.IEquipment.IsBroken = true;
                }
            }
        }

        public void OnTimedEvent(object sender, EventArgs e)
        {
            
            IsBroken();

            for (int i = 0; i < Track.Sections.Count; i++)
            {
                
                KeyValuePair<Section, SectionData> entry = _positions.ElementAt(i);
                SectionData sd = GetSectionData(entry.Key);

                System.Threading.Tasks.Parallel.Invoke
                    (
                        () => UpdateLeft(sd, entry, i),
                        () => UpdateRight(sd, entry, i)
                    );

                if(sd.Left != null || sd.Right != null)
                {
                    DriversChanged(new DriversChangedEventArgs(Track));
                }

                if (DriversFinished.Count == Data.CurrentRace.Participants.Count)
                {
                    FinishRace();
                }
            }
        }

        private void UpdateLeft(SectionData sd, KeyValuePair<Section, SectionData> entry, int i)
        {
            if (sd.Left != null && !Participants[0].IEquipment.IsBroken)
            {
                int speedParticipant = 0;
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


                       
                    }
                    else
                    {
                        KeyValuePair<Section, SectionData> entryNext = _positions.ElementAt(0);
                        sd = GetSectionData(entryNext.Key);
                        sd.Left = Participants[0];
                        sd.DistanceLeft = 0;

                    }
                    if (entry.Key.SectionType.Equals(SectionTypes.Finish))
                    {
                        LapsLeft++;
                        if (LapsLeft == Laps)
                        {
                            sd.Left = null;
                            DriversChanged(new DriversChangedEventArgs(Track));

                            DriversFinished.Enqueue(participants[0]);
                        }
                    }

                }

            }
        }

        private void UpdateRight(SectionData sd, KeyValuePair<Section, SectionData> entry, int i)
        {
            if (sd.Right != null && !Participants[1].IEquipment.IsBroken)
            {
                int speedParticipant = 0;
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
                      
                    }
                    else
                    {
                        KeyValuePair<Section, SectionData> entryNext = _positions.ElementAt(0);
                        sd = GetSectionData(entryNext.Key);

                        sd.Right = Participants[1];
                        sd.DistanceRight = 0;
                      


                    }
                    if (entry.Key.SectionType.Equals(SectionTypes.Finish))
                    {
                        LapsRight++;
                        if (LapsRight == Laps)
                        {
                            sd.Right = null;
                            DriversChanged(new DriversChangedEventArgs(Track));

                            DriversFinished.Enqueue(participants[1]);
                        }
                    }

                }

            }
        }
        public void Start()
        {
            timer.Start();
        }

        public void FinishRace()
        {
            timer.Elapsed -= OnTimedEvent;
            Console.Clear();
            timer.Close();
            RaceFinishedEvent?.Invoke(this, EventArgs.Empty);

        }

        public Queue<IParticipant> Ranking()
        {
            return DriversFinished;
        }
    }
}
