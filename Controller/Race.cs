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

        private int Laps = 3;
        public event DriversChanged DriversChanged;
        public event RaceFinished RaceFinishedEvent;
        private Queue<IParticipant> DriversFinished;

        private Dictionary<IParticipant, long> _sectionTimes;
        private Dictionary<IParticipant, int> _quality;
        private Dictionary<IParticipant, int> _rounds;
        private Dictionary<IParticipant, TimeSpan> _timeBroken;

        public Race(Track track, List<IParticipant> participants)
        {
           
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            StartTime = DateTime.Now;
            DriversFinished = new Queue<IParticipant>();
            _positions = new Dictionary<Section, SectionData>();
            
            RaceFinishedEvent += Data.Competition.OnRaceFinished;
            timer.Elapsed += OnTimedEvent;
            _rounds = new Dictionary<IParticipant, int>();
            _sectionTimes = new Dictionary<IParticipant, long>();
            _quality = new Dictionary<IParticipant, int>();
            _timeBroken = new Dictionary<IParticipant, TimeSpan>();

            AddParticipantsToTrack(track, participants);
            Start();
        }

        public void AddParticipantsToTrack(Track track, List<IParticipant> participants)
        {
            int count = 0;
            RandomizeEquipment();
            var TrackSections = track.Sections;
                foreach (Section s in TrackSections)
                {
                    if (s.SectionType == SectionTypes.StartGrid && count < participants.Count)
                    {
                        SectionData sectionData = GetSectionData(s);
                        sectionData.Left = participants[count];
                        _rounds.Add(Participants[count], 0);
                    sectionData.DistanceLeft = 0;
                        count++;
                        sectionData.Right = participants[count];
                        _rounds.Add(Participants[count], 0);
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

        private void SetRandomBroken(ElapsedEventArgs args)
        {
            foreach (IParticipant participant in Participants)
            {
                int randomizer = _random.Next(1, 100);
                if (randomizer % participant.IEquipment.Quality == 3)
                {
                    SetTimeBroken(participant, args);
                    participant.IEquipment.IsBroken = true;
                }
            }
        }
        private void SetUnBroken()
        {
            foreach (IParticipant participant in Participants)
            {
                if (participant.IEquipment.IsBroken)
                {
                    if (_random.Next(1, 5) == 2)
                    {
                        participant.IEquipment.IsBroken = false;
                        if (participant.IEquipment.Quality > 2)
                        {
                            participant.IEquipment.Quality--;
                        }
                        else if (participant.IEquipment.Performance > 15)
                        {
                            participant.IEquipment.Performance--;
                        }
                    }
                }
            }
        }
   
        private void SetTimeBroken(IParticipant participant, ElapsedEventArgs args)
        {
            TimeSpan timeSpan = new TimeSpan(args.SignalTime.Ticks - StartTime.Ticks);
            if (timeSpan != TimeSpan.Zero)
            {
                Data.Competition.ParticipantsTimeBroken.Add(new ParticipantsTimeBroken()
                { Participant = participant, TimeSpan = timeSpan, Track = Track });
            }
        }
        private void SaveTimeBroken()
        {
            foreach (var VARIABLE in _timeBroken)
            {
                Data.Competition.ParticipantsTimeBroken.Add(new ParticipantsTimeBroken() { Participant = VARIABLE.Key, TimeSpan = VARIABLE.Value, Track = Track });
            }
        }

        private void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.IEquipment.Performance = _random.Next(5, 10);
                participant.IEquipment.Quality = _random.Next(1, 5);
                _quality.Add(participant, participant.IEquipment.Quality);
            }
        }

        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {

            for (int i = 0; i < Track.Sections.Count; i++)
            {
                
                KeyValuePair<Section, SectionData> entry = _positions.ElementAt(i);
                SectionData sd = GetSectionData(entry.Key);

                System.Threading.Tasks.Parallel.Invoke
                    (
                        () => UpdateLeft(sd, entry, i, e),
                        () => UpdateRight(sd, entry, i, e)
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
            SetRandomBroken(e);
            SetUnBroken();
        }

        private void UpdateLeft(SectionData sd, KeyValuePair<Section, SectionData> entry, int i, ElapsedEventArgs e)
        {
            if (sd.Left != null && !Participants[0].IEquipment.IsBroken)
            {
                int speedParticipant = 0;
                speedParticipant = sd.Left.IEquipment.Speed * sd.Left.IEquipment.Performance;


                sd.DistanceLeft += speedParticipant;
                if (sd.DistanceLeft > 100)
                {
                    SaveTimeForSectionAndParticipant(entry.Key, sd.Left, e);
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
                        _rounds[sd.Left]++;
                        if (_rounds[sd.Left] == Laps)
                        {
                            sd.Left = null;
                            DriversChanged(new DriversChangedEventArgs(Track));

                            DriversFinished.Enqueue(participants[0]);
                            SaveQuality(participants[0]);
                        }
                    }
                    
                }

            }
        }

        private void UpdateRight(SectionData sd, KeyValuePair<Section, SectionData> entry, int i, ElapsedEventArgs e)
        {
            if (sd.Right != null && !Participants[1].IEquipment.IsBroken)
            {
                int speedParticipant = 0;
                speedParticipant = sd.Right.IEquipment.Speed * sd.Right.IEquipment.Performance;


                sd.DistanceRight += speedParticipant;
                if (sd.DistanceRight > 100)
                {
                    SaveTimeForSectionAndParticipant(entry.Key, sd.Right, e);
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
                        _rounds[sd.Right]++;
                        if (_rounds[sd.Right] == Laps)
                        {
                            sd.Right = null;
                            DriversChanged(new DriversChangedEventArgs(Track));

                            DriversFinished.Enqueue(participants[1]);
                            SaveQuality(participants[1]);
                        }
                    }
                    
                }

            }
        }
        public void Start()
        {
            foreach (IParticipant deelnemer in Participants)
            {
                _sectionTimes.Add(deelnemer, 0);
                _timeBroken.Add(deelnemer, TimeSpan.Zero);
            }
            timer.Start();
        }

        public void FinishRace()
        {
            timer.Elapsed -= OnTimedEvent;
            Console.Clear();
            timer.Close();
            SaveTimeBroken();
            RaceFinishedEvent?.Invoke(this, new RaceFinishedArgs() { Ranking = DriversFinished, Track = track }); 

        }

        public Queue<IParticipant> Ranking()
        {
            return DriversFinished;
        }

        private void SaveTimeForSectionAndParticipant(Section section, IParticipant participant, ElapsedEventArgs args)
        {
            long ticksLastRound = _sectionTimes[participant];
            if (ticksLastRound == 0)
            {
                ticksLastRound = StartTime.Ticks;
            }
            TimeSpan timeSpan = new TimeSpan(args.SignalTime.Ticks - ticksLastRound);
            _sectionTimes[participant] = timeSpan.Ticks;
            Data.Competition.ParticipantsSectionTimes.Add(new ParticipantSectionTimes() { Participant = participant, Section = section, SectionTime = timeSpan });
        }
        private void SaveQuality(IParticipant participant)
        {
            Data.Competition.ParticipantsQuality.Add(new ParticipantsQuality() { Participant = participant, QualityBeforeRace = _quality[participant], QualityAfterRace = participant.IEquipment.Quality, Track = Track });
        }
    }
}
