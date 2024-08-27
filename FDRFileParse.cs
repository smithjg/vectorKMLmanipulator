using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DisplayBixlerPath
{
    /// <summary>
    /// This class has knowledge of the shape of the FDR file and can abstract the components.
    /// </summary>
    public class FDRFileParse
    {
        List<ReadingPoint> ReadingPointList = new List<ReadingPoint>();
        ActiveSessionIdentifier? _activeSessionIdentifier = null;
        ReadingPointHeader? readingPointHeader;// in reality JGS we should not make this alowable to null

        /// Constructor 
        public FDRFileParse(string[] lines)
        {
            _activeSessionIdentifier = new ActiveSessionIdentifier();
            readActionOnFDRFile(lines);
        }


        public ObservableCollection<string> SessionNames()
        {
            return _activeSessionIdentifier.SessionNames();
        }


        /// need to account for the first two lines JGS 
        private void readActionOnFDRFile(string[] lines)
        {
            for (var i = 2; i < (lines.Length); i++)
            {
                if (lines[i].Contains("Session"))
                {   // the lines starting with session are a part of a pair
                    // where the second line is the start and stop index 
                    // And the first line is the name of the session 
                    _activeSessionIdentifier.AddWSession(lines[i], lines[i + 1]);
                    i++; // we have read a second line so increment the counter to account for that 
                }
                else if (lines[i].StartsWith("Milliseconds"))
                {   // a special case of a Reading point where the component values are the names of the components
                    readingPointHeader = new ReadingPointHeader(lines[i]);
                }
                else
                {
                    UInt16 _sessionId = _activeSessionIdentifier.givenLineReturnSessionID(i); // i is not the right variable yet 
                    ReadingPoint rp = new ReadingPoint(_sessionId, lines[i]);
                    
                    ReadingPointList.Add(rp);
                    // ReadingPoint ReadingPointLast = ReadingPointList.Last();
                }
            }
        }


        /// Function extracts just the coordinates from each reading point in the file
        public List<CoordinateTriple> ExtractKMLPathGivenSession(UInt16 sessionID)
        {
            List<CoordinateTriple> _listCoordTriple = new List<CoordinateTriple>();

            if (0 == sessionID)
            {
                _listCoordTriple = ExtractAllKMLPath();
            }
            else
            {
                foreach (ReadingPoint var in ReadingPointList)
                {
                    if (var.SessionIndex == sessionID)
                    {
                        CoordinateTriple cordTripple = new CoordinateTriple(var, 26, 25, 11);// jgs magic numbers To Eradicate 
                        _listCoordTriple.Add(cordTripple);
                    }
                }
            }

            return _listCoordTriple;
        }

        /// Function extracts just the coordinates from each reading point in the file
        /// There is no distinction in the different sessions here 
        /// this needs to go - its just for testing 
        public List<CoordinateTriple> ExtractAllKMLPath()
        {
            List < CoordinateTriple > _listCoordTriple = new List<CoordinateTriple> ();

            foreach (ReadingPoint var in ReadingPointList)
            {
                CoordinateTriple cordTripple = new CoordinateTriple(var,26,25,27);
                _listCoordTriple.Add(cordTripple);
            }

            return _listCoordTriple;
        }

    }
}
