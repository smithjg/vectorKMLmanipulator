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
        string? _LatLonAltstring;
        
        /// <summary>
        /// this is where we divorce from the concept of a reading point and map to the concept of kml
        /// </summary>
        public CoordinateTriple(ReadingPoint readingPoint,byte lat,byte lon,byte alt)
        {
            _LatLonAltstring = readingPoint.GivenHandleFindComponent(lat) + ','+ readingPoint.GivenHandleFindComponent(lon) + ','+ readingPoint.GivenHandleFindComponent(alt);
        }

        public string CoordinateTripleAsKMLCoord()
        {   // check for null before returning the coordinate sting in kml format 
            return (_LatLonAltstring==null)?" ":_LatLonAltstring;
        }
    }
}
