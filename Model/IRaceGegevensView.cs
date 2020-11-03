using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Model
{
    public interface IRaceGegevensView
    {
        public IParticipant Participant { get; set; }
        public void Add(List<IRaceGegevensView> list);
        public string GetBestParticipant(List<IRaceGegevensView> list);
    }
}
