using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisplayBixlerPath
{
    /// A class that maps from the text version of the item in the telementry to an index that can be used to access the value in subsequent data points.
    public class ReadingPointHeader
    {

        ReadingPoint _readingPoint;
        byte _latHandle;
        byte _lonHandle;
        byte _altHandle;


        /// The simple get going approach 
        public ReadingPointHeader(string phrase)
        { 
            _readingPoint = new ReadingPoint(phrase);
            _latHandle = _readingPoint.GivenStringFindComponent("GPSLat");
            _lonHandle = _readingPoint.GivenStringFindComponent("GPSLon");
            _altHandle = _readingPoint.GivenStringFindComponent("GPSAlt");
        }

        /// To be used in a more complex approach 
        public byte GetHandleForTitle(string title)
        {
            return _readingPoint.GivenStringFindComponent(title);
        }
         
    }
}
