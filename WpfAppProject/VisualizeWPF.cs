using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using System.Windows.Media.Imaging;

namespace WpfAppProject
{
    public static class VisualizeWPF
    {
        private static int orientation;

        static int x = 0;
        static int y = 3;

        static int GraphicHeight = 128;
        static int GraphicWidth = 128;

        public static int Orientation
        {
            get => orientation;
            set
            {
                orientation = value;
                if (orientation > 3)
                {
                    orientation = 0;
                }
                else if (orientation < 0)
                {
                    orientation = 3;
                }
            }
        }

       

        public static BitmapSource DrawTrack(Track track)
        {
            var empty = ImageRender.GetBitmap(6* GraphicWidth, 8* GraphicHeight);
            foreach (Section section in track.Sections)
            {
                

                int cursorX = x * GraphicWidth;
                int cursorY = y  * GraphicHeight;

                // subtract minX and minY in order to make sure the lowest index is always 0
               

                string tileGraphic = SectionTileToGraphic(section.SectionType, Orientation);



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

                Bitmap sectionGFX = ImageRender.GetBitmapUrl(tileGraphic);

                Graphics gfx = Graphics.FromImage(empty);
                gfx.DrawImage(sectionGFX, cursorX, cursorY, 128, 128);

                // get the right graphic
                SectionData sectionDataParticipants = Data.CurrentRace.GetSectionData(section);

               
               

                if (sectionDataParticipants.Left != null)
                {
                    if (sectionDataParticipants.Left.IEquipment.IsBroken)
                    {
                        Bitmap car = ImageRender.GetBitmapUrl(_carBroken);
                        gfx.DrawImage(car, cursorX + 20, cursorY + 20, 39, 65);
                       
                    }
                    else
                    {
                        Bitmap car = ImageRender.GetBitmapUrl(carColor(sectionDataParticipants.Left.TeamColor));

                        gfx.DrawImage(car, cursorX + 20, cursorY + 20, 39, 65);
                    }
                    
                }
                if (sectionDataParticipants.Right != null)
                {

                    if (sectionDataParticipants.Right.IEquipment.IsBroken)
                    {
                        Bitmap car = ImageRender.GetBitmapUrl(_carBroken);
                       

                        gfx.DrawImage(car, cursorX + 70, cursorY + 20, 39, 65);
                    }
                    else
                    {
                        Bitmap car = ImageRender.GetBitmapUrl(carColor(sectionDataParticipants.Right.TeamColor));
                        
                        gfx.DrawImage(car, cursorX + 70, cursorY + 20, 39, 65);
                    }
                }

                // draw the graphic in place



                x += -(Orientation - 2) % 2;
                y += (Orientation - 1) % 2;

            }


            return ImageRender.CreateBitmapSourceFromGdiBitmap(empty);
        }

        

        private static string SectionTileToGraphic(SectionTypes type, int orientation)
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

        private static string carColor(TeamColors teamColor)
        {
            return teamColor switch
            {
                TeamColors.Blue => _carBlue,
                TeamColors.Red => _carRed,
                TeamColors.Yellow => _carYellow,
                TeamColors.Green => _carGreen,
                TeamColors.Grey => _carGrey,
            };
        }

        #region graphics
        private const string _finishNorth = @"..\..\..\\images\\finishNorth.png";
        private const string _finishEast = @"..\..\..\\images\\finishEast.png";
        private const string _finishSouth = @"..\..\..\\images\\finishSouth.png";
        private const string _finishWest = @"..\..\..\\images\\finishWest.png";

        private const string _startGridNorth = @"..\..\..\\images\\startGridNorth.png";
        private const string _startGridEast = @"..\..\..\\images\\startGridEast.png";
        private const string _startGridSouth = @"..\..\..\\images\\startGridSouth.png";
        private const string _startGridWest = @"..\..\..\\images\\startGridWest.png";

        private const string _cornerSE = @"..\..\..\\images\\cornerSE.png";
        private const string _cornerSW = @"..\..\..\\images\\cornerSW.png";
        private const string _cornerNE = @"..\..\..\\\images\\cornernE.png";
        private const string _cornerNW = @"..\..\..\\images\\cornerNW.png";

        private const string _straightHorizontal = @"..\..\..\\images\\straightHorizontal.png";
        private const string _straightVertical = @"..\..\..\\images\\straightVertical.png";

        private const string _carBlue = @"..\..\..\\images\\carBlue.png";
        private const string _carGreen = @"..\..\..\\images\\carGreen.png";
        private const string _carGrey = @"..\..\..\\images\\carGrey.png";
        private const string _carRed = @"..\..\..\\images\\carRed.png";
        private const string _carYellow = @"..\..\..\\images\\carYellow.png";
        private const string _carBroken = @"..\..\..\\images\\carBroken.png";
        #endregion

    }
}
