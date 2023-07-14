using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBixlerPath
{
    /// <summary>
    /// This class has knowledge of the shape of the FDR file and can abstract the components.
    /// </summary>
    public class FDRFileParse
    {
        List<ReadingPoint> ReadingPointList = new List<ReadingPoint>();
        List<SessionIdentifier> SessionIdentifierList = new List<SessionIdentifier>();
        ReadingPointHeader? readingPointHeader;

        public FDRFileParse(string[] lines)
        {
            readActionOnFDRFile(lines);
        }

        private void readActionOnFDRFile(string[] lines)
        {
            for (var i = 0; i < (lines.Length); i++)
            {
                if (lines[i].Contains("Session"))
                {   // the lines starting with session are a part of a pair
                    // where the second line is the start and stop index 
                    // And the first line is the name of the session 
                    SessionIdentifierList.Add(new SessionIdentifier(lines[i], lines[i + 1]));
                    i++; // we have read a second line so increment the counter to account for that 
                }
                else if (lines[i].StartsWith("Milliseconds"))
                {   // a special case of a Reading point where the component values are the names of the components
                    readingPointHeader = new ReadingPointHeader(lines[i]);
                }
                else
                {   // The reading points - they are 29 entries of strings representing the values of the measured components 
                    // They dont always exist in pairs but they can be read two at a time - so check for the 
                    ReadingPointList.Add(new ReadingPoint(lines[i]));
                }
            }
            ReadingPoint ReadingPointLast = ReadingPointList.Last();
        }

        public bool ExtractKMLCoordinates(SessionIdentifier sessionIdentifier)
        {

            return true;
        }

        public List<CoordinateTriple> ExtractKMLCoordinates()
        {
            List < CoordinateTriple > _listCoordTriple = new List<CoordinateTriple> ();

            foreach (ReadingPoint readingPoint in ReadingPointList)
            {
                CoordinateTriple cordTripple = new CoordinateTriple(readingPoint);
                _listCoordTriple.Add(cordTripple);
            }

            return _listCoordTriple;
        }

    }
}
