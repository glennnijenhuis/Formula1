using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfAppProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RaceStats RaceStats;
        private ParticipantsCompetionStats ParticipantStats;
       
        public MainWindow()
        {
   
            Data.Initialize();
            StartRace();
           
            InitializeComponent();
        }

        public void StartRace()
        {
          
            Data.NextRace();
            Data.CurrentRace.RaceFinishedEvent += OnRaceFinishedWPF;
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += WPFDriversChanged;
                
            }
        }

        public void OnRaceFinishedWPF(object source, RaceFinishedArgs raceFinishedArgs)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (RaceStats != null)
                    RaceStats.Close();
                if (ParticipantStats != null)
                    ParticipantStats.Close();
            });
            Data.CurrentRace.DriversChanged -= WPFDriversChanged;
            Data.CurrentRace.RaceFinishedEvent -= OnRaceFinishedWPF;

            ImageRender.ClearCache();
            StartRace();
            
        }

        public void WPFDriversChanged(DriversChangedEventArgs e)
        {
            
            this.TrackGrid.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.TrackGrid.Source = null;
                    this.TrackGrid.Source = VisualizeWPF.DrawTrack(e.Track);
                }));

           
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenParticipantStats(object sender, RoutedEventArgs e)
        {
            ParticipantStats = new ParticipantsCompetionStats();

            ParticipantStats.Show();
            
        }

        private void OpenRaceStats(object sender, RoutedEventArgs e)
        {
            RaceStats = new RaceStats();
            RaceStats.Show();
        }
    }
}
