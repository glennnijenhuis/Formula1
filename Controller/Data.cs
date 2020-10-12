﻿using System;
using Model;

namespace Controller
{
    public static class Data
    {
        private static Competition competition;
        private static Race currentRace;
        public static Competition Competition { get => competition; set => competition = value; }
        public static Race CurrentRace { get => currentRace; set => currentRace = value; }

        public static void Initialize()
        {
           Competition = new Competition();
           AddParticipants();
           AddTracks();

        }
        public static void AddParticipants()
        {
           

            Competition.Participants.Add(new Driver("Max Verstappen", 0, new Car(9,9,9,false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Lando Norris", 0, new Car(8, 8, 8, false), TeamColors.Red));

        }

        public static void AddTracks()
        {

            Track track1 = new Track("Circuit Heerde", new SectionTypes[] {SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Finish });
            Track track2 = new Track("Circuit Zandvoort", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Finish, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner,  SectionTypes.Straight, SectionTypes.LeftCorner });
            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);


        }

        public static void NextRace()
        {
            Track track = Competition.NextTrack();

            if(track != null)
            {
                CurrentRace = new Race(track, Competition.Participants);
            }
        }
    }
}
