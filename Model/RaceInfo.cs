using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class RaceInfo<T> where T : IRaceGegevensView
    {
        private List<IRaceGegevensView> _list = new List<IRaceGegevensView>();

        public void Add(T obj)
        {
            _list.Add(obj);
        }

        public string Print()
        {
            if (_list.Any())
            {
                return _list[1].GetBestParticipant(_list);
            }
            else
            {
                return "Leeg";
            }
        }
    }
}
