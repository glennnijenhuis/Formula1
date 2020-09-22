using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1
{
    public static class Visualize
    {
        public static void Initialize()
        {
            
        }

        public static void DrawTrack(Track track)
        {

        }

        #region graphics
        private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _finishVertical = { "|  |", "|##|", "|  |", "|  |" };
        private static string[] _startGridHorizontal = { "----", "  | ", "  | ", "----" };
        private static string[] _startGridVertical = { "|  |", "|--|", "|  |", "|  |" };
        private static string[] _leftCornerDownEntry = { @"--\ ", @"  \ ", @"\  |", "|  |" };
        private static string[] _rightCornerDownEntry = { @" /--", @"/   ", @"|  /", "|  |" };
        private static string[] _leftCornerUpEntry = { @"|  |", @"|  \", @"\   ", @" \--" };
        private static string[] _rightCornerUpEntry = { @"|  |", @"/  |", @"   /", "--/ " };
        private static string[] _straightHorizontal = { "----", "    ", "    ", "----" };
        private static string[] _straightVertical = { "|  |", "|  |", "|  |", "|  |" };
        #endregion
    }
}
