using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Coach : Settler
    {
        private static int _coachNb = 0;

        /// <summary>
        /// Constructor that allows you to create a coach
        /// </summary>
        public Coach() : base() 
        {
            _type = "C";
            _coachNb++;
            _id = _type + _coachNb.ToString();
            _decreasingHunger = 5;
            _decreasingEnergy = 5;
        }
    }
}
