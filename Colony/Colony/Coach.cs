using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colony
{
    class Coach : Settler
    {
        private int _coachId;
        private static int _coachNb = 0;
        public Coach() : base() 
        {
            _coachNb++;
            _coachId = _coachNb;
        }

        public override string ToString()
        {
            return base.ToString() + "C'est le coach n° : " + _coachId + "\n";
        }

    }
}
