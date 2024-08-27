using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DisplayBixlerPath
{
    public  class SessionIdentifier
    {// could check that the format is correct 
        string _name;
        string[] _markers;
        public UInt16 _startLine; /// jgs fix 
        public UInt16 _finishLine;
        UInt16 _sessionId;

        public SessionIdentifier(string line1,string line2,UInt16 sessionID)
        {
            _name = line1;
            _markers = line2.Split(' ');// worth checking there are only 2 
            _sessionId = sessionID; 

            try
            {
                _startLine = UInt16.Parse(_markers[0]);
                _finishLine = UInt16.Parse(_markers[1]);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public UInt16 Start => _startLine;     // the property where the session starts 
        public UInt16 Finish => _finishLine;    // the Name property where the session ends
        public string SessionName => _name;     // the Name property 
        public UInt16 SessionId => _sessionId;     // the SessionID property 
        public bool givenLineReturnSessionID(int line)
        {
            return ((line >= Start) && (line <= Finish))?true:false; 
        }
    }
}
