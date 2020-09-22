using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Section
    {
        private SectionTypes sectionType;

        public SectionTypes SectionType { get => sectionType; set => sectionType = value; }


        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }
    }

    public enum SectionTypes
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }
}
