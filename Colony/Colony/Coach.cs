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
        }

        public override void Play()
        {
            _energyState -= 5;
            if (_energyState < 0)
                _energyState = 0;
            _hungerState -= 5;
            if (_hungerState < 0)
                _hungerState = 0;
        }



    }
}
