using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Track
    {
        private string name;
        private LinkedList<Section> sections;

        public string Name { get => name; set => name = value; }
        public LinkedList<Section> Sections { get => sections; set => sections = value; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = ArrayToLinkedList(sections);
           
        }

        public LinkedList<Section> ArrayToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> linkedListSection = new LinkedList<Section>();
            
            foreach (SectionTypes sec in sections)
            {
                linkedListSection.AddLast(new Section(sec));
            }

            return linkedListSection;
        }
    }
}
