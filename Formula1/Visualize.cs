using Model;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Controller;
namespace Formula1
{
    public static class Visualize
    {
        
        static int x = 5;
        static int y = 10;
        static int minX = 1;
        static int minY = 1;

        private static int orientation;

        public static int Orientation { get => orientation;
            set
            {
                orientation = value;
                if (orientation > 3)
                {
                    orientation = 0;
                } else if (orientation < 0)
                {
                    orientation = 3;
                }
            } 
        }

        public static void Initialize()
        {
            Data.CurrentRace.DriversChanged += HandleDriversChanged;
        }

        public static void DrawTrack(Track track)
        {
            
            foreach (Section section in track.Sections)
            {
                
                // subtract minX and minY in order to make sure the lowest index is always 0

                int GraphicHeight = 4;
                int GraphicWidth = 7;

                int cursorX = (x - minX) * GraphicWidth;
                int cursorY = (y - minY) * GraphicHeight;

                string[] tileGraphic = SectionTileToGraphic(section.SectionType, Orientation);
                

                if (tileGraphic == _cornerNW)
                {
                    if (Orientation == 0)
                    {
                        Orientation--;
                    }
                    else
                    {
                        Orientation++;
                    }


                }
                else if (tileGraphic == _cornerNE)
                {

                    if (Orientation == 0)
                    {
                        Orientation++;
                    }
                    else
                    {
                        Orientation--;
                    }

                }
                else if (tileGraphic == _cornerSW)
                {
                    if (Orientation == 2)
                    {
                        Orientation++;
                    }
                    else
                    {
                        Orientation--;
                    }

                }
                else if (tileGraphic == _cornerSE)
                {
                    if (Orientation == 2)
                    {
                        Orientation--;
                    }
                    else
                    {
                        Orientation++;
                    }



                }


                // get the right graphic
                SectionData sectionDataParticipants = Data.CurrentRace.GetSectionData(section);

                // draw the graphic in place
                for (int i = 0; i < GraphicHeight; i++)
                {
                    string lane = tileGraphic[i];

                    lane = SetupPositions(lane, sectionDataParticipants.Left, sectionDataParticipants.Right);
                    
                    Console.SetCursorPosition(cursorX, cursorY + i);
                    Console.Write(lane);
                }




                x += -(Orientation - 2) % 2;
                y += (Orientation - 1) % 2;

            }
        }

 

        private static string[] SectionTileToGraphic(SectionTypes type, int orientation)
        {
            return type switch
            {
                SectionTypes.Straight => (orientation % 2) switch
                {
                    0 => _straightVertical,
                    1 => _straightHorizontal,
                    _ => throw new ArgumentOutOfRangeException()
                },
                SectionTypes.LeftCorner => orientation switch
                {
                    0 => _cornerNW,
                    1 => _cornerSW,
                    2 => _cornerSE,
                    3 => _cornerNE,
                    _ => throw new ArgumentOutOfRangeException()
                },
                SectionTypes.RightCorner => orientation switch
                {
                    0 => _cornerNE,
                    1 => _cornerNW,
                    2 => _cornerSW,
                    3 => _cornerSE,
                    _ => throw new ArgumentOutOfRangeException()
                },
                SectionTypes.StartGrid => orientation switch
                {
                    0 => _startGridNorth,
                    1 => _startGridEast,
                    2 => _startGridSouth,
                    3 => _startGridWest,
                    _ => throw new ArgumentOutOfRangeException()
                },
                SectionTypes.Finish => orientation switch
                {
                    0 => _finishNorth,
                    1 => _finishEast,
                    2 => _finishSouth,
                    3 => _finishWest,
                    _ => throw new ArgumentOutOfRangeException()
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static string SetupPositions(string naam, IParticipant participant1, IParticipant participant2)
        {
            if (naam.Contains("1"))
            {
                if (participant1 != null)
                {
                    string naamParticipant = participant1.Name;
                    string naam2 = "";
                    naam2 = naamParticipant.Substring(0, 1);
                    naam = naam.Replace("1", naam2);
                    return naam;
                }
                else
                {
                    naam = naam.Replace("1", " ");
                    return naam;
                }
            }
            else if (naam.Contains("2"))
            {
                if (participant2 != null)
                {
                    string naamParticipant = participant2.Name;
                    string naam2 = "";
                    naam2 = naamParticipant.Substring(0, 1);
                    naam = naam.Replace("2", naam2);
                    return naam;
                }
                else
                {
                    naam = naam.Replace("2", " ");
                    return naam;
                }
            }
            else
            {
                return naam;
            }
        }

        public static void HandleDriversChanged(DriversChangedEventArgs driversChangedEventArgs)
        {
            DrawTrack(driversChangedEventArgs.Track);

        }

        #region graphics
        private static string[] _finishNorth = {"|     |",
                                                "|#####|",
                                                "| 1   |",
                                                "|   2 |" };
        private static string[] _finishEast = { "-------",
                                                " #   2  ",
                                                " # 1    ",
                                                "-------" };
        private static string[] _finishSouth = {"|    |",
                                                "|    |",
                                                "|####|",
                                                "|    |" };
        private static string[] _finishWest = { "-------",
                                                "   1 # ",
                                                " 2   # ",
                                                "-------" };

        private static string[] _startGridNorth = { "| _   |",
                                                    "| 1 _ |",
                                                    "|   2 |",
                                                    "|     |" };
        private static string[] _startGridEast = {  "-------",
                                                    "   1]  ",
                                                    " 2]    ",
                                                    "-------" };
        private static string[] _startGridSouth = { "|   2 |",
                                                    "| 1 _ |",
                                                    "| _   |",
                                                    "|     |" };
        private static string[] _startGridWest = {  "-------",
                                                    "   [2  ",
                                                    " [1    ",
                                                    "-------" };

        private static string[] _cornerSE = {   @"|     -",
                                                @"|   1  ", 
                                                @"| 2    ",
                                                @"-------" };
        private static string[] _cornerSW = {   @"-     |",
                                                @"  1   |", 
                                                @"    2 |",
                                                @"-------" };
        private static string[] _cornerNE = {   @"-------",
                                                @"| 2    ",
                                                @"|   1  ",
                                                @"|     -" };
        private static string[] _cornerNW = {   @"-------",
                                                @"    2 |",
                                                @"  1   |",
                                                @"-     |" };

        private static string[] _straightHorizontal = { "-------", "   1   ", "   2   ", "-------" };
        private static string[] _straightVertical = { "|     |", "| 1   |", "|   2 |", "|     |" };

       
        #endregion
    }
}
