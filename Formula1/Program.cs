using System;
using System.Threading;
using Controller;

namespace Formula1
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Visualize.DrawTrack(Data.CurrentRace.Track);

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
