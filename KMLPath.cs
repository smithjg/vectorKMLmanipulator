using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DisplayBixlerPath
{
    /// <summary>
    /// A list of coordinates that creates a path when dropped into Google Earth 
    /// Derived from a list of Data plot points that is related to the 
    /// </summary>
    public class KMLPath
    {
        string pathPreamble  = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <kml xmlns=""http://www.opengis.net/kml/2.2"">
                          <Document>
                            <name>Paths</name> 
                            <description>Examples of paths. Note that the tessellate tag is by default 
                              set to 0. If you want to create tessellated lines, they must be authored 
                              (or edited) directly in KML.</description> 
                            <Style id=""yellowLineGreenPoly""> 
                              <LineStyle> 
                                <color>7f00ffff</color>
                                <width>4</width> 
                              </LineStyle> 
                              <PolyStyle> 
                                <color>7f00ff00</color> 
                              </PolyStyle>
                            </Style>
                            <Placemark>
                              <name>Absolute Extruded</name>
                              <description>Transparent green wall with yellow outlines</description>
                              <styleUrl>#yellowLineGreenPoly</styleUrl>
                              <LineString>
                                <extrude>1</extrude>
                                <tessellate>1</tessellate>
                                <altitudeMode>absolute</altitudeMode>
                                <coordinates> ";
  
        string pathend = @"        </coordinates>      </LineString>    </Placemark>  </Document></kml> ";
        StringBuilder kmlPathdefinition;

        /// Given a set of coordinates create a KML format Path 
        public KMLPath( List < CoordinateTriple > coordTripleList)
        {
            kmlPathdefinition = new StringBuilder(pathPreamble);
            foreach(var coordTriple in coordTripleList)
            {
                kmlPathdefinition.AppendLine(coordTriple.CoordinateTripleAsKMLCoord());// of course dereive from header jgs 
            }
            kmlPathdefinition.AppendLine(pathend);
        }

        /// Write the path out to a file
        public string GetPath()
        {
            return kmlPathdefinition.ToString();
        }

    }
}
