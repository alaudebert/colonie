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
        public Coach() : base() 
        {
            _type = "coach_";
            _coachNb++;
            _id = _type + _coachNb.ToString();
            _decreasingHunger = 5;
            _decreasingEnergy = 5;
        }
    }
}
