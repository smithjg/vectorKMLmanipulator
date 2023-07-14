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

        public SessionIdentifier(string line1,string line2)
        {
            _name = line1;
            _markers = line2.Split(' ');// worth checking there are only 2 
        }

        public string Start => _markers[0];     // the property where the session starts 
        public string Finish => _markers[1];    // the Name property where the session ends
        public string SessionName => _name;     // the Name property 
    }
}
