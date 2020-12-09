using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controller
{
    public class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string CurrentTrack => Data.CurrentRace.Track.Name;
        public string ParticipansQuality => Data.Competition.ParticipantsPoints.Print();
        public Dictionary<IParticipant,int> Rounds => Data.CurrentRace._rounds;
        public TimeSpan TotalTime => Data.CurrentRace.stopwatch.Elapsed;
        public Dictionary<IParticipant, long> SectionTimes => Data.CurrentRace._sectionTimes;
        public List<IParticipant> test => Data.CurrentRace.Participants;
        public string BestLap => Data.Competition.ParticipantsSectionTimes.Print();
        public DataContext()
        {
            Data.CurrentRace.DriversChanged += DataContextHandler;
            
        }

        public void DataContextHandler(DriversChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
            
        }
       
    }
}
