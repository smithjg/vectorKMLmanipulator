using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayBixlerPath
{
    public class ReadingPointHeader
    {

        ReadingPoint _readingPoint;
        byte _latHandle;
        byte _lonHandle;
        byte _altHandle;

        public ReadingPointHeader(string phrase)
        { 
            _readingPoint = new ReadingPoint(phrase);
            _latHandle = _readingPoint.GivenStringFindComponent("GPSLat");
            _lonHandle = _readingPoint.GivenStringFindComponent("GPSLon");
            _altHandle = _readingPoint.GivenStringFindComponent("GPSAlt");
        }

        public byte GetHandleForTitle(string title)
        {
            return _readingPoint.GivenStringFindComponent("GPSAlt");
        }

         
    }
}
