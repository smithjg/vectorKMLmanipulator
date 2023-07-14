using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DisplayBixlerPath
{
    public  class CoordinateTriple
    {
        //string? _longitude;
        //string? _latitude;
        //string? _altitude;
        ReadingPoint? _readingPoint; //instead of making the stings we keep a reference to them 

        /// <summary>
        /// Should we need a tripple created from raw data 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="altitude"></param>
        //CoordinateTriple(string longitude, string latitude, string altitude)
        //{// we are coping the reference I believe 
        //    _longitude = longitude;
        //    _latitude = latitude;
        //    _altitude = altitude;
        //    _readingPoint = null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readingPoint"></param>
        public CoordinateTriple(ReadingPoint readingPoint)
        {// we are copying the reference I believe 
            _readingPoint = readingPoint;
        }

        public string CoordinateTripleAsKMLCoord(byte lat, byte lon,byte alt)
        {
            return (_readingPoint==null)?" ":_readingPoint.GivenHandleFindComponent(lat) + ','+ _readingPoint.GivenHandleFindComponent(lon) + ','+ _readingPoint.GivenHandleFindComponent(alt);
        }
    }
}
