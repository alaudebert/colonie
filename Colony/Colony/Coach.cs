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
        private static string _type;
        public Coach() : base()
        {
            _type = "C";
            SettlerType = _type;
            _coachNb++;
            _id = _type + _coachNb.ToString();
            _decreasingHunger = 3;
            _decreasingEnergy = 3;
        }

        public void Lead(int turnNb)
        {
            if (!IsInActivity)
            {
                NbTunrBeforeAvailable = 4 + turnNb;
                IsInActivity = true;
            }

            if (NbTunrBeforeAvailable == turnNb)
            {
                IsInActivity = false;
                NbTunrBeforeAvailable = 0;
            }
        }
        public string Type
        {
            get { return _type; }
        }
    }
}
