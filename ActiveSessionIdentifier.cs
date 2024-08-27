using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DisplayBixlerPath
{
    public class ActiveSessionIdentifier

    {
        private List<SessionIdentifier> _SessionIdentifierList;
        private UInt16 _sessionID = 0;


        /// <summary>
        /// Constructor 
        /// </summary>
        /// <returns></returns>
        public ActiveSessionIdentifier()
        {
            _SessionIdentifierList = new List<SessionIdentifier>();
        }

        /// <summary>
        /// when parsing the file a set of session information is contained in two lines  
        /// Use the information to create and add to the list of session information 
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        public bool AddWSession(string line1, string line2)
        {
            bool retval = false;
            if (_SessionIdentifierList != null)
            {
                _SessionIdentifierList.Add(new SessionIdentifier(line1, line2, _sessionID));
                _sessionID++;
                retval = true;
            }
            return retval;
        }

        // Not a nice way to do this but return the session Id for a line number 
        public UInt16 givenLineReturnSessionID(int line)
        {
            foreach (SessionIdentifier sessionIdentifier in _SessionIdentifierList)
            {
                if (sessionIdentifier.SessionId != 0)
                {
                    if (true == sessionIdentifier.givenLineReturnSessionID(line))
                    {
                        return sessionIdentifier.SessionId;
                    }
                }
            }

            return 0;
        }

        public ObservableCollection<string> SessionNames()
        {
            ObservableCollection<string> s = new ObservableCollection<string>();
            foreach (SessionIdentifier sessionIdentifier in _SessionIdentifierList)
            {
                s.Add(sessionIdentifier.SessionName);
            }
            return s;
        }

    }
}

