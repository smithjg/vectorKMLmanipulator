using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace DisplayBixlerPath
{
    public class ReadingPoint
    {
        string[] _components;
        bool _valid = false;
 
        /// <summary>
        /// Returns if the object is a valid Reading point - there are some null ones 
        /// </summary>
        public bool Valid => _valid;
        public UInt16 SessionIndex
        { get; set; }


        public ReadingPoint(string phrase)
        {
            _components = phrase.Split(' ');
            if ((_components.Length != 0) || (_components.Length == 29))// JGS Magic number 
            {
                _valid = true;
            }
        }

        //constructor
        public ReadingPoint(UInt16 sessionId,string phrase):this(phrase)
        {
            SessionIndex = sessionId;
        }

        /// <summary>
        /// If the Reading point is valid it matches a field value
        /// returns max val for unkonwn value 
        /// </summary>
        /// <param name="componentValue"></param>
        /// <returns></returns>
        public byte GivenStringFindComponent(string componentValue)
        {
            byte i = 0;
            if (Valid != true) return byte.MaxValue;
            for (; i < (_components.Length) ; i++)
            {
                if (_components[i].StartsWith(componentValue)) break;
            }
            return i;
        }

        /// 
        public string GivenHandleFindComponent(byte componentHandle)
        {// check the array bounds of course 
            return (componentHandle<=_components.Length)?_components[componentHandle]:String.Empty;
        }

        public bool SessionStart()
        {
            bool retVal = false;
            if (this.Valid)
            {
                if (("50" == GivenHandleFindComponent(2)) && ("23" == GivenHandleFindComponent(3))) /// jgs magic numbers to remove
                {
                    retVal = true;
                }
            };
            return retVal;
        }
    }

  
}
