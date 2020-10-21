using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceInfo<T>
    {
        private Queue<T> _list;

        public void Add(T obj)
        {
            _list.Enqueue(obj);
        }

        public string Print()
        {
            if (_list.Count != 0)
            {
                return "HUH";
            }
            else
            {
                return "Leeg for some reason";
            }
        }
    }
}
