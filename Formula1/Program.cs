using System;
using System.Threading;
using Controller;
using Model;

namespace Formula1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            StartRace();

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }

        static void StartRace()
        {
            
            Data.NextRace();
            Visualize.Initialize();
            Data.CurrentRace.RaceFinishedEvent += OnRaceFinished;
            Visualize.DrawTrack(Data.CurrentRace.Track);
            
        }

        public static void OnRaceFinished(object source, RaceFinishedArgs raceFinishedArgs)
        {
            StartRace();
        }
    }
}
